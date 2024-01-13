SELECT
	REFS.rowid, 
	MEMBER_1.name AS SRC,
	MEMBER_2.name AS SRC
FROM
	xrefs AS REFS
LEFT JOIN memberdef MEMBER_1
	ON MEMBER_1.rowid = REFS.src_rowid
LEFT JOIN memberdef MEMBER_2
	ON MEMBER_2.rowid = REFS.dst_rowid;
