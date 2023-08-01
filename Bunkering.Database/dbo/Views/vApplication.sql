CREATE VIEW dbo.vApplication
AS
SELECT dbo.Applications.Id, dbo.ApplicationTypes.Id AS AppId, dbo.ApplicationTypes.Name AS ApplicationName, dbo.Inspections.Id AS InspectionId, dbo.Inspections.InspectionDocument, dbo.SubmittedDocuments.Id AS SubmittedDocId, 
                  dbo.SubmittedDocuments.DocName, dbo.SubmittedDocuments.DocType, dbo.AppFees.ApplicationFee, dbo.FacilityTypes.Name AS FacilityTypeName
FROM     dbo.AppFees INNER JOIN
                  dbo.ApplicationTypes ON dbo.AppFees.ApplicationTypeId = dbo.ApplicationTypes.Id INNER JOIN
                  dbo.Applications ON dbo.ApplicationTypes.Id = dbo.Applications.ApplicationTypeId INNER JOIN
                  dbo.FacilityTypes ON dbo.AppFees.FacilityTypeId = dbo.FacilityTypes.Id INNER JOIN
                  dbo.Inspections ON dbo.Applications.ApplicationTypeId = dbo.Inspections.ApplicationId INNER JOIN
                  dbo.SubmittedDocuments ON dbo.Applications.Id = dbo.SubmittedDocuments.ApplicationId
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vApplication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'       Column = 1440
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
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vApplication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[59] 4[3] 2[25] 3) )"
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
         Top = -240
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AppFees"
            Begin Extent = 
               Top = 343
               Left = 229
               Bottom = 506
               Right = 446
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "ApplicationTypes"
            Begin Extent = 
               Top = 221
               Left = 6
               Bottom = 372
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Applications"
            Begin Extent = 
               Top = 0
               Left = 229
               Bottom = 193
               Right = 446
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "FacilityTypes"
            Begin Extent = 
               Top = 490
               Left = 0
               Bottom = 631
               Right = 194
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Inspections"
            Begin Extent = 
               Top = 60
               Left = 476
               Bottom = 204
               Right = 711
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "SubmittedDocuments"
            Begin Extent = 
               Top = 251
               Left = 483
               Bottom = 414
               Right = 677
            End
            DisplayFlags = 280
            TopColumn = 3
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
  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vApplication';

