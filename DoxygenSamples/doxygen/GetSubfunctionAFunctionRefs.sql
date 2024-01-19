SELECT
	REFS.rowid, 
	MEMBER_1.name AS SRC_NAME,
	MEMBER_2.TYPE AS DST_TYPE,
	MEMBER_2.name AS DST_NAME,
	MEMBER_2.argsstring AS ARGS
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
		MEMBER_1.file_id = MEMBER_1.bodyfile_id
	AND
		MEMBER_2.file_id = MEMBER_2.bodyfile_id
	AND
		MEMBER_2.kind = 'function'
	AND
		SRC_NAME = 'main';
