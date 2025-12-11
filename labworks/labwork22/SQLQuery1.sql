SELECT LastEnter,
	CASE
		WHEN CHARINDEX('/', Import1_User.LastEnter) > 0 THEN 
		FORMAT(TRY_CONVERT(DATE, Import1_User.LastEnter, 101), 'dd.MM.yyyy') -- MM/dd/yyyy 
		ELSE Import1_User.LastEnter
		END AS Enter
FROM Import1_User;
