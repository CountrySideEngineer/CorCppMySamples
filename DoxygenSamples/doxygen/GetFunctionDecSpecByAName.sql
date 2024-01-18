SELECT
	memberdef.rowid,
	memberdef.type AS TYPE,
	memberdef.name AS FUNC_NAME,
	memberdef.argsstring AS ARGUMENTS,
	compounddef.name AS FILE_NAME
FROM 
	memberdef
LEFT JOIN compounddef
	ON compounddef.file_id = memberdef.bodyfile_id
WHERE
	FUNC_NAME = 'SubFunction_001_01'
AND
	FILE_NAME = 'SubCode_001_01.cpp'
LIMIT
	1;
	