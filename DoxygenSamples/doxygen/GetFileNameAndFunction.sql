SELECT 
	compounddef.name AS FILE_NAME,
	memberdef.name AS FUNC_NAME,
	memberdef.type AS FUNC_TYPE,
	memberdef.argsstring AS FUNC_ARG
FROM
	memberdef
LEFT JOIN compounddef
ON compounddef.file_id = memberdef.file_id
WHERE memberdef.file_id = memberdef.bodyfile_id
ORDER BY FILE_NAME;
	