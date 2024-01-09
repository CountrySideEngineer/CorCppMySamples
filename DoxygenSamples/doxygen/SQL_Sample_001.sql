SELECT 
	refid.rowid, 
	refid.refid, 
	xrefs.src_rowid, 
	memberdef.name AS SRC_NAME, 
	xrefs.dst_rowid, 
	memberdef.name AS DST_NAME
FROM xrefs
LEFT OUTER JOIN refid No1
	ON SRC1.src_rowid = No1.rowid
LEFT OUTER JOIN refid No2
	ON SRC1.dst_rowid = No2.rowid
LEFT OUTER JOIN memberdef
	ON memberdef.rowid = xrefs.src_rowid;