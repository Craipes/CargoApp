SELECT TOP 100 *
      FROM [Settlements] AS [s]
      WHERE ([s].[IsVisible] = CAST(1 AS bit)) AND (('²Çß' LIKE N'') OR (CHARINDEX(N'²Çß', [s].[NormalizedSettlement]) > 0))
      ORDER BY [s].[City]