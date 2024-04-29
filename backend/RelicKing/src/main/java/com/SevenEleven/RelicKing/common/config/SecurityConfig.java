package com.SevenEleven.RelicKing.common.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

import com.SevenEleven.RelicKing.common.security.CustomAuthenticationFilter;
import com.SevenEleven.RelicKing.common.security.JWTFilter;
import com.SevenEleven.RelicKing.common.security.JWTUtil;
import com.SevenEleven.RelicKing.repository.MemberRepository;

import lombok.RequiredArgsConstructor;

@Configuration
@EnableWebSecurity
@RequiredArgsConstructor
public class SecurityConfig {

	private final AuthenticationConfiguration authenticationConfiguration;
	private final JWTUtil jwtUtil;
	private final MemberRepository memberRepository;

	private final String[] whiteList = {
		"/api/members/login",
		"/api/members/temp-password",
		"/api/members/duplicate-email",
		"/api/members/duplicate-nickname",
		"/api/members/email-code",
		"/api/members/email-authenticate",
		"/api/members/signup",
		"/api/members/kakao-login",
	};

	@Bean
	public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {

		http
			.csrf(AbstractHttpConfigurer::disable)    // csrf disable
			.formLogin(auth -> auth.loginPage("/api/members/login"))    // form 로그인 방식 disable
			.httpBasic(AbstractHttpConfigurer::disable)    // http basic 인증 방식 disable

			// 경로별 인가 작업
			.authorizeHttpRequests(auth -> auth
				.requestMatchers(whiteList).permitAll()
				.anyRequest().authenticated())

			// 필터 추가
			.addFilterBefore(new JWTFilter(jwtUtil, memberRepository), CustomAuthenticationFilter.class)
			.addFilterAt(new CustomAuthenticationFilter(authenticationManager(authenticationConfiguration), jwtUtil), UsernamePasswordAuthenticationFilter.class)

			// 세션 사용하지 않음
			.sessionManagement(session ->
				session.sessionCreationPolicy(SessionCreationPolicy.STATELESS));

		return http.build();
	}

	@Bean
	public BCryptPasswordEncoder bCryptPasswordEncoder() {
		return new BCryptPasswordEncoder();
	}

	@Bean
	public AuthenticationManager authenticationManager(AuthenticationConfiguration configuration) throws Exception {
		return configuration.getAuthenticationManager();
	}
}
