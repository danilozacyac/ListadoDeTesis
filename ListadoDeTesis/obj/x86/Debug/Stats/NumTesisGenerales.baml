   M S B A M L   `   `   `  ����  ListadoDeTesis,    &ListadoDeTesis.Stats.NumTesisGenerales   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��600��5      $	��400��5      $��NumTesisGenerales��5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��45 �6   �� 5      $��* �6   .+�5   
   0  *Telerik.Windows.Controls.RadCartesianChart  -   5          Name$ TotalesAbogado��5   $   $��1��5   $   $��Stretch=�5   $   $��Stretch=�5   $   5  /Telerik.Windows.Controls.ChartView.RadChartBase   Palette>  8Telerik.Windows.Controls.ChartView.ChartPaletteConverter$ Windows8 5   $      HorizontalAxis 5      8  2Telerik.Windows.Controls.ChartView.CategoricalAxis  5         VerticalAxis 5      3  -Telerik.Windows.Controls.ChartView.LinearAxis  5         Series. 5      2  ,Telerik.Windows.Controls.ChartView.BarSeries  -   5          Name$ 
TesisPleno��5       :  4Telerik.Windows.Controls.ChartView.CategoricalSeries   CategoryBindingJ  DTelerik.Windows.Controls.ChartView.StringToDataPointBindingConverter$ Usuario 5          ValueBinding$ 
TotalTesis 5       4	  .Telerik.Windows.Controls.ChartView.ChartSeries	 	  ItemsSource	 5       �� 
   LegendSettings
 5       =
  7Telerik.Windows.Controls.ChartView.SeriesLegendSettings
  5!       
  Title$ Pleno��63   5#        5$      -   5        $ TesisPrimera��5$       $ Usuario 5%       $ 
TotalTesis 5'       	 5&       �� 
 5(      
  5)      $ Primera Sala��63   5+        5,      -   5        $ TesisSegunda��5,       $ Usuario 5-       $ 
TotalTesis 5/       	 5.       �� 
 50      
  51      $ Segunda Sala��63   53        54      -   5        $ TesisPlenosC��54       $ Usuario 55       $ 
TotalTesis 57       	 56       �� 
 58      
  59      $ P. de Circuito��63   5;        5<      -   5        $ TesisTribunales��5<       $ Usuario 5=       $ 
TotalTesis 5?       	 5>       �� 
 5@      
  5A      $ T. Colegiados ��63   5C      5D      (  "Telerik.Windows.Controls.RadLegend  5E   
   $��1��6   $��Right=�5F      $	��Top=�5G      
�������
���   �$��1q�5J         Items 5K      �� *LegendItems+ �� ElementName$ TotalesAbogado���� 5M   
   -   5         ��Name$ TxtTitle��5M      $��1��5N      $
��0,15q�5O      $��Center=�5P      $	��Top=�5Q      $��16(�5R      $��Black"�5S      $��Tesis enviadas en��5T      * 
 $Telerik.Windows.Controls.RadComboBox  5V   
   -	   5          Name$ CbxYear��5V      $	��108��5W      $��	22,10,0,0q�5X      $
��Left=�5Y      $	��Top=�5Z      5[      5\      5]      