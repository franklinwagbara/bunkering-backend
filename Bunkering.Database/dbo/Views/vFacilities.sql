CREATE VIEW dbo.vFacilities
AS
SELECT dbo.Facilities.Name, dbo.Facilities.Address, dbo.Facilities.IsLicensed, dbo.FacilityTypes.Name AS FacilityTypeName, dbo.Companies.Name AS ComapnyName, dbo.Tanks.Name AS TankName, dbo.Tanks.Capacity, dbo.Facilities.Id, 
                  dbo.Companies.Id AS CompanyId, dbo.Tanks.Id AS TanksId, dbo.Products.Id AS ProductId, dbo.Products.Name AS ProductName, dbo.FacilityTypes.Id AS Expr5
FROM     dbo.Products INNER JOIN
                  dbo.FacilityTypes INNER JOIN
                  dbo.Tanks INNER JOIN
                  dbo.Facilities ON dbo.Tanks.FacilityId = dbo.Facilities.Id INNER JOIN
                  dbo.Companies ON dbo.Facilities.CompanyId = dbo.Companies.Id ON dbo.FacilityTypes.Id = dbo.Facilities.FacilityTypeId ON dbo.Products.Id = dbo.Tanks.ProductId
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vFacilities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vFacilities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[65] 4[3] 2[29] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Products"
            Begin Extent = 
               Top = 224
               Left = 332
               Bottom = 343
               Right = 526
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "FacilityTypes"
            Begin Extent = 
               Top = 34
               Left = 530
               Bottom = 175
               Right = 724
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tanks"
            Begin Extent = 
               Top = 190
               Left = 23
               Bottom = 353
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Facilities"
            Begin Extent = 
               Top = 10
               Left = 276
               Bottom = 193
               Right = 470
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Companies"
            Begin Extent = 
               Top = 2
               Left = 6
               Bottom = 165
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vFacilities';

