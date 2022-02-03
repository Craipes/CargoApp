SELECT TOP 100 *
      FROM [Settlements] AS [s]
      WHERE ([s].[IsVisible] = CAST(1 AS bit)) AND (('���' LIKE N'') OR (CHARINDEX(N'���', [s].[NormalizedSettlement]) > 0))
      ORDER BY [s].[City]