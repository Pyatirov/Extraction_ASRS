   M S B A M L   `   `   `  ����?  ;ASRS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ASRSASRS  5   	       ASRS.Researcher   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:ASRS     5   	   -   5          � Title$   Исследователь��5      $	��700��6#   $	��500��60    � 
ResizeMode$ NoResize=�6<    � 	Resources 5	      #   ASRS.RowToVisibilityConverter   RowToVisibilityConverter��%   &	          5
   
   6)   5      .��5      � .+�5   
   �� .@�5      �� $��Эксперимент��6   .��5      �  � RowDefinitions 5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   �� 5      $��* �6(   �� 5      $
��Auto �6(   �� 5      $
��Auto �6(    � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� $��0��6    $'��!Модель экстракции��6-   $
��10,5q�6F   �� 5      -   5         ��Name$ cb_Mechanisms_Experiment��5      $��1��6A   $��
10,0,10,10q�6N   $��25��6b    �� DisplayMemberPath$
 Info��6n   6�   �� 5      $��2��6#   $��0��60   $��2��6@   $
��Auto=�6T   $��Disabled=�6w   .��5       A� -   5         A�Name$ ug_Constants_Inputs_Panel��5    &    A� Columns$ 1��6I   $��10q�6U   $	��Top=�6a   $��Center=�6y   5!      �� 5#      -   5        $��3��5#   8   $��0��6E   $��2��6U   $��Расчет��6i   $��10,10q�6z   $��40��6�   $	��200��6�   �� 5$      -   5        $��0��5$   D   $��2��6Q   $��1��6b   $B��<Посмотреть компонентную матрицу��6r   $��40��6�   $	��210��6�   5&      5'      �� 5(      $'��!Модели экстракции��6   .��5)      �  5*      �� 5+      $��* �6(   �� 5,      $
��Auto �6(   .+�5/      &   System.Windows.Controls.DataGrid  -   5        	  Name$	 dg_Mechanisms��5/      
   AutoGenerateColumns$
 False��66   $��
10,10,10,0q�6R   $��0��6f   $��2��6s   "   VerticalScrollBarVisibility$ Visible=�6�      Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *ID+6T     52      $ Описание��61   $ * 6\    6C   �� *Info+6\   4  .System.Windows.Controls.DataGridTemplateColumn  53      $
 Auto 65      CellTemplate 54   "   �� 55   &   .R�56   *   �� -   5        $��Удалить��56   1   $	��5 2q�6C   $��3q�6Q   
 � Tag 6~   �� *ID+y�6�   ��  �� 	Converter# [  57   '   59      5;      �� 5<      -   5        $#��Добавить модель��5<      $��10,10,10,10q�6V   $��1��6k   $��2��6x   $��Center=�6�   $��40��6�   $	��200��6�   $	��Top=�6�   5=      5>      �� 5?      $3��-Экспериментальные точки��6   .��5@      �  5A      �� 5B      $
��Auto �6(   �� 5C      $
��Auto �6(   �� 5D      $��* �6(   �� 5E      $
��Auto �6(   .+�5H      �� $��0��6    $3��-Выберите модель реакции:��6-   $
��10,5q�6M   �� 5I      -	   5        $ cb_Mechanisms_Points��5I      $��1��6=   $��	10,0,0,10q�6J   $��Center=�6]   $
 Info��6x   $
��Left=�6�   $��25��6�   $	��252��6�   �� 5K      $��2��6#   $
��Auto=�60   $��Disabled=�6S   .��5L      �� -
   5         ��Name$ pointInputsPanel��5L   %   $��10q�6?   $	��Top=�6K   $��Center=�6c   5M      �� 5O      -   5        $��3��5O      $!��Добавить точку��6*   $��10,10q�6C   $��40��6R   $	��200��6^   6j   5P      5Q      5R      5S      5T      