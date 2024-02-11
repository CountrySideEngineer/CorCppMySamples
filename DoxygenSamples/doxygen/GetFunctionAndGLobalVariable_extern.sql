SELECT
	REFS.rowid, 
	MEMBER_1.rowid AS SRC_ROW_ID,
	MEMBER_1.name AS SRC,
	MEMBER_2.rowid AS DST_ROW_ID,
	MEMBER_2.name AS DST
FROM
	xrefs AS REFS
LEFT JOIN memberdef MEMBER_1
	ON MEMBER_1.rowid = REFS.src_rowid
LEFT JOIN memberdef MEMBER_2
	ON MEMBER_2.rowid = REFS.dst_rowid
LEFT JOIN refid REFID_1
	ON REFID_1.rowid = REFS.src_rowid
LEFT JOIN refid REFID_2
	ON REFID_2.rowid = REFS.dst_rowid
WHERE 
		MEMBER_1.bodyfile_id = MEMBER_1.file_id
	AND
		MEMBER_2.extern = 1
	AND
		MEMBER_2.kind = 'variable'
;
