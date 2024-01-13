SELECT
	FUNC_MEMBER_PARAM_DEF.rowid AS FUNC_ROW_ID,
	FUNC_MEMBER_PARAM_DEF.memberdef_id AS FUNC_DEF_ID,
	FUNC_MEMBER_DEF.type AS FUNC_TYPE,
	FUNC_MEMBER_DEF.name AS FUNC_NAME,
	ARG_MEMBER_DEF.type AS ARG_TYPE,
	ARG_MEMBER_DEF.declname AS ARGNAME
FROM
	memberdef_param AS FUNC_MEMBER_PARAM_DEF
LEFT OUTER JOIN memberdef FUNC_MEMBER_DEF
	ON FUNC_MEMBER_DEF.rowid = FUNC_MEMBER_PARAM_DEF.memberdef_id
LEFT OUTER JOIN param ARG_MEMBER_DEF
	ON ARG_MEMBER_DEF.rowid = FUNC_MEMBER_PARAM_DEF.param_id
WHERE FUNC_MEMBER_DEF.file_id = FUNC_MEMBER_DEF.bodyfile_id;