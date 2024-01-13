SELECT
	REFS.rowid, 
	REF1.refid AS SRC,
	REF2.refid AS DST
FROM
	xrefs AS REFS
LEFT JOIN refid REF1
	ON REF1.rowid = REFS.src_rowid
LEFT JOIN refid REF2
	ON REF2.rowid = REFS.dst_rowid;