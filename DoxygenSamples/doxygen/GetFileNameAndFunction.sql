SELECT 
	memberdef.rowid,
	compounddef.name AS FILE_NAME,
	memberdef.scope AS CLASS_NAME,
	memberdef.name AS FUNC_NAME,
	memberdef.type AS FUNC_TYPE,
	memberdef.argsstring AS FUNC_ARG,
	memberdef.definition AS FUNC_DEF,
	memberdef.argsstring AS FUNC_ARGS
FROM
	memberdef
LEFT JOIN compounddef
ON compounddef.file_id = memberdef.bodyfile_id
WHERE memberdef.kind = 'function' AND compounddef.kind = 'file'
ORDER BY FILE_NAME;
	