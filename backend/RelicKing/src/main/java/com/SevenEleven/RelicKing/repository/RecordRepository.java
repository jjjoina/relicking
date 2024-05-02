package com.SevenEleven.RelicKing.repository;

import java.util.Optional;

import org.springframework.data.jpa.repository.EntityGraph;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import com.SevenEleven.RelicKing.entity.Member;
import com.SevenEleven.RelicKing.entity.Record;

public interface RecordRepository extends JpaRepository<Record, Integer> {

	@EntityGraph(attributePaths = {"recordRelics", "recordSkills"}, type = EntityGraph.EntityGraphType.FETCH)
	Optional<Record> findByMemberAndStage(Member member, int stage);

	@EntityGraph(attributePaths = {"recordRelics", "recordSkills"}, type = EntityGraph.EntityGraphType.FETCH)
	@Query("select r from Record r where r.recordId = :recordId")
	Optional<Record> findByRecordId(int recordId);
}
