   M S B A M L   `   `   `  ����  ListadoDeTesis    ListadoDeTesis.AddTesis   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      -   5        $	��763��5      $	��650��5      $��Agregar Tesis��5      )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5	      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5
       � 	Resources 5         buttonstripe1 �� 
TargetType �� Property �� Value �� 
TargetType � Cursor �� CornerRadius	 �� 
StartPoint
 �� EndPoint � Offset � Color �� Triggers P� Property P� Value �� 
TargetName��%  &	        �� 5   
   " 	�6&   .�5      ��  � �� 5      �� 5      " 	�6&   .R�5      �� $
 Hand��6*   ._�5      �� $	��120��6%   $��20��5   %   ���	#FFE2E2E2$��1q�5   %   $ 3��5   %   ._�5   "   � .+�5   &   �� $��Center=�67   $��Center=�6T   �� 5   &   $��	RadioMark��6-   $��10��5   -   $��5,0,48,0q�5   -   $
��Left=�5   -   ���	#FFCDCDCD$ 1��5   -   5!   #   ��5#   "   �� 5$   &   $	 0.5,0&�6:   $
 0.5,1&�6M   .��5%   *   � $ 0Q�67   $ White��6B   � 5&   *   $ 1Q�67   $ 	#FFE2E2E2��6B   5'   '   5)      5*       5-      P� 5.       �@d�	IsChecked$
 trueY�6;   . �5/   "   �� $ 	RadioMark��6)    � ��
 � ���6V   50      52      54      55      56      .��57      �  � RowDefinitions 58   
   �� 59      $��* �6   �� 5:      $
��Auto �6   �� 5;      $��50 �6   .+�5=   
   $  Telerik.Windows.Controls.Label  $��	43,40,0,0q�6   $
��Left=�5>      $	��Top=�5?      $��Núm. de tesis:��5@      �� 5A   
   -   5         ��Name$ TxtNumTesis��5A      $	��146��5B      $��23��5C      $��
145,39,0,0q�5D      $
��Left=�5E      $	��Top=�5F       �� TextWrapping$
 Wrap=�5G      -  'Telerik.Windows.Controls.RadRadioButton  5H   
   -   5          Name$ RadCorte��5H   !   $��83��5I   !   $��
420,40,0,0q�5J   !   $
��Left=�5K   !   $	��Top=�5L   !   $��SCJN ��5N   !     5O   
   -   5        $ 	RadPlenos��5O   !   $��83��5P   !   $��
518,40,0,0q�5Q   !   $
��Left=�5R   !   $	��Top=�5S   !   $��Plenos��5U   !     5V   
   -   5        $ RadTribunal��5V   !   $��83��5W   !   $��
617,40,0,0q�5X   !   $
��Left=�5Y   !   $	��Top=�5Z   !   $	��TCC��5\   !   * 
 $Telerik.Windows.Controls.RadComboBox  5]   
   -   5          Name$ CbxSubInstancia��5]      $	��280��5^      $��
420,88,0,0q�5_      $
��Left=�5`      $	��Top=�5a         
IsEditable .    IsFilteringEnabled .    TextSearchMode$ Contains=�5d       �� DisplayMemberPath$ 	Organismo��5e      {�.   �� SelectedValuePath$ IdOrganismo��5h      ��5g      �� 5i      �� 5j   
   -   5        $ TxtRubro��5j      $	��657��5k      $��43,192,0,15q�5l      $
��Left=�5m      $��Stretch=�5n       �� AcceptsReturn . $
 Wrap=�5p      �� 5q   
   $��
43,126,0,0q�6   $
��Left=�5r      $	��Top=�5s      $��Color:��5t        5u   
   -   5        $ CbxColor��5u      $	��146��5v      $��145,130,0,0q�5w      $
��Left=�5x      $	��Top=�5y      $ Color��5z      $ IdColor��5|      ��5{      �� ��5}      �� 5~      .�5      ��  �@�ToolTip 68   �� *Descripcion+5�      5�      	� 5�   
   -	   5         	�Name$ 
RadAislada��5�      $��	43,94,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $�� T. Aislada��5�       	� 	GroupName$
 Tipo��5�      #��[  5�      	� 5�   
   -
   5        $ RadJuris��5�      $��
171,94,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $�� Jurisprudencia��5�      $
 Tipo��5�       d� 	IsChecked$
 TrueY�5�      #��[  5�      �� 5�   
   $��420,130,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Fecha de envío:��5�      , 
 &Telerik.Windows.Controls.RadDatePicker  5�   
   -   5          Name$ DpFechaEnvio��5�       $	��182��5�       $��518,132,0,0q�5�       $
��Left=�5�       $	��Top=�5�       �� 5�   
   $��
43,166,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Título y subtítulo:��5�      $��14(�5�      $
��Bold"�5�      � 5�   
   -   5          �Name$  MateriasAsignadas��5�      $��1��5�      $	��200��5�      $y�	Collapsed=�5�      .+�5�      �� $	��250��6   $��30��5�      $��	43,15,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $"��Materia sugerida por la Sala��5�      $��14(�5�      $
��Bold"�5�      �� 5�      -   5        $ TxtMaterias��5�      $��43,45,43,45q�5�      $��Stretch=�5�      $��Stretch=�5�       . 5�      �� 5�      $��
43,164,0,0q�6   $
��Left=�5�      $	��Top=�5�      $��Oficio:��5�      �� 5�      -   5        $ 	TxtOficio��5�      $	��175��5�      $��23��5�      $��
92,164,0,0q�5�      $
��Left=�5�      $	��Top=�5�      $
 Wrap=�5�      (  "Telerik.Windows.Controls.RadButton  5�      -   5        !  Name$! BtnMateriasSga��5�       $	��145��5�       $��555,166,0,0q�5�       $
��Left=�5�       $	��Top=�5�       $��Ver Materias��5�       5�        5�   
   -   5        $! 
BtnGuardar��5�      $��2��5�      $��83��5�      $��33��5�      $��	0,0,43,10q�5�      $��Right=�5�      $��Bottom=�5�      $��Guardar��5�        5�   
   -   5        $! BtnCancelar��5�      $��2��5�      $��83��5�      $��33��5�      $��
0,0,131,10q�5�      $��Right=�5�      $��Bottom=�5�      $��Cancelar��5�      " �� IsCancel" . 5�      5�      5�      