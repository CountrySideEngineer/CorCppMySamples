SELECT 
	compounddef.name AS FILE_NAME,
	path.name AS FILE_PATH
FROM 
	compounddef
LEFT JOIN path
	ON 
		path.rowid = compounddef.file_id
WHERE
	compounddef.kind = 'file'
	AND
	substr(FILE_NAME, -4, 4) = '.cpp' OR
	substr(FILE_NAME, -2, 2) = '.c'
	

	
