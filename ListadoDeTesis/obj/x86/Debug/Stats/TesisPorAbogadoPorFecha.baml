   M S B A M L   `   `   `  ����I  EListadoDeTesis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:ListadoDeTesis.ConvertersListadoDeTesis.Converters  5      2    ,ListadoDeTesis.Stats.TesisPorAbogadoPorFecha   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      =	converter'clr-namespace:ListadoDeTesis.Converters     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��800��5      $	��600��5      $��TesisPorAbogadoPorFecha��5      (  "Telerik.Windows.Controls.RadWindow    WindowStartupLocation$  CenterScreen=�5
       � 	Resources 5      1   +ListadoDeTesis.Converters.RowColorConverter   rowColor2   ,ListadoDeTesis.Converters.FontColorConverter  	fontColor��%:   &	        &	        5   
   6&     5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   �� 5      $��40 �6   �� 5      $��* �6   .+�5   
   , 
 &Telerik.Windows.Controls.RadDatePicker  -   5          Name$ DpFechaDeEnvio��5       $	��173��5       $��
215,18,0,0q�5       $
��Left=�5       $	��Top=�5       5       0  *Telerik.Windows.Controls.RadCartesianChart  5   
   $��1��6$   $��Stretch=�5   $   $��Stretch=�5   $   5  /Telerik.Windows.Controls.ChartView.RadChartBase   Palette>  8Telerik.Windows.Controls.ChartView.ChartPaletteConverter$ Arctic 5   $      HorizontalAxis 5       8  2Telerik.Windows.Controls.ChartView.CategoricalAxis  5!         VerticalAxis 5#      3	  -Telerik.Windows.Controls.ChartView.LinearAxis	  5$         Series. 5&      2
  ,Telerik.Windows.Controls.ChartView.BarSeries
  -   5         
 Name$ TesisLawyer��5&       :  4Telerik.Windows.Controls.ChartView.CategoricalSeries	   CategoryBindingJ  DTelerik.Windows.Controls.ChartView.StringToDataPointBindingConverter$	 Usuario 5'       $��Segoe UI)�5(       $��10(�5)       
   ValueBinding$
 
TotalTesis 5+       4  .Telerik.Windows.Controls.ChartView.ChartSeries   ItemsSource 5*       ��  
  LegendSettings 5,      =  7Telerik.Windows.Controls.ChartView.SeriesLegendSettings  5-         Title$  Total de tesis por abogado��63   5/      50      �� 51   
   $��	10,14,0,0q�6   $
��Left=�52      $	��Top=�53      $#��Fecha de envío de las tesis:��54      $��14(�55      $
��Bold"�56      �� 57   
   $��2��6   $��24,7,0,0q�58      $
��Left=�59      $	��Top=�5:      $��Filtrar por abogado:��5;      $��14(�5<      $
��Bold"�5=      * 
 $Telerik.Windows.Controls.RadComboBox  5>   
   -   5          Name$ CbxAbogados��5>      $��2��5?      $	��250��5@      $��
215,10,0,0q�5A      $
��Left=�5B      $	��Top=�5C       �� DisplayMemberPath$ Nombre��5D       �� SelectedValuePath$ 	IdUsuario��5F      ��5E      �� 5G      *  $Telerik.Windows.Controls.RadGridView  5H   
   -   5        $��GTesis��5H      $��3��5I      $��Stretch=�5J      $��Stretch=�5K      ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   AutoGenerateColumns .     CanUserDeleteRows .     CanUserInsertRows .     ShowGroupPanel .  * 	 $Telerik.Windows.Controls.DataControl   ItemsSource 5O      ��    Columns 5Q      1  +Telerik.Windows.Controls.GridViewDataColumn  5R      -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 100 6-   6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Núm. tesis��5T   -      IsFilterable .     
IsReadOnly .    DataMemberBinding 5S   -   �� *
ClaveTesis+5V   -     5W      $ * 6-   $ Rubro��5Y   -    .  5X   -   �� *Rubro+5Z   -     5[      $
 Auto 6-      DataFormatString$  {0:dd/MM/yyyy}��5\   -   $ Fecha de Alta��5^   -    .   .  5]   -   �� *FechaAltaSistema+5`   -      RowStyle 5c      �� 5d       �� 
TargetType3  -Telerik.Windows.Controls.GridView.GridViewRow"  6   .�5e      ��  �� Property � ��  �� Value  63   �� *Tatj+! �� 	Converter#! [  �� 5f       � ��  63   �� *	IdColor+#! [ 5g      5i      5j      5k      