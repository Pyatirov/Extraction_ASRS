   M S B A M L   `   `   `  ����?  ;ASRS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ASRSASRS  5   	       ASRS.AddMechanism   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:ASRS     5   	   -   5          � Title$+  %Настройка механизма��5      $	��450��6)   $	��800��66    � 
ResizeMode$ NoResize=�6B    � 	Resources 5	      $   ASRS.NullToVisibilityConverter   NullToVisibilityConverter#   ASRS.OneToVisibilityConverter  OneToVisibilityConverter   BooleanToVisibilityConverter��%W   &	        &	      &	 &       5
   
   6*     5   
   6)   �� 5   
   6'   5      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $$��Описание модели:��6   $��Center=�6,   $��	10,10,0,5q�6G   $��0��6Z   �� 5   
   -   5         ��Name$ tb_Mechanism_Name��5      $��	10,5,10,5q�6-   $��1��6@   $��2��6M   �� 5   
   -   5        $:��4Ввести компонентную матрицу��5      $��2��67   $��40��6D   $	��200��6P   $
��10 5q�6\   $��2��6k   6   �� 5   
   $&�� Выберите реакции:��6   $��3��6-   $��Center=�6:   $
��Left=�6U   $��10,5,0,0q�6p   $��16��6�   $	��106��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dg_Reactions��5         AutoGenerateColumns$ False��6)      SelectionMode$ Single=�5       	   CanUserAddRows$	 False��6   $��4��5!      $��
10,10,10,0q�6   $��2��6(   
   Columns
 5"      4  .System.Windows.Controls.DataGridCheckBoxColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ Выбрать��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6:   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *
IsSelected+ �� Mode$ TwoWay=�4  .System.Windows.Controls.DataGridTemplateColumn  5$      $ Реакция��6)   $ * 6:      CellTemplate 5%      �� 5&      .R�5'      ��  �� Orientation$ 
Horizontal=�6)   $��Center=�6B   .+�5)   "   �� $ 
Horizontal=�6-   y�5*       �� *HasReagent1+ �� 	Converter# [ .+�5+   &   �� $��0,0,2,0q�6N   ��60   �� *KInp1Display+6N   �� 5,   &   ��60   �� *Reaction.Inp1+�� 5-   &   $��Bottom=�6M   $��2,-2,0,0q�6h   $��12(�6z   ��60   �� *Ind1Display+6z   5.   #   �� 51   "   $	�� + ��6,   y�67   �� *	ShowPlus1+# [ �� 54   "   $ 
Horizontal=�6-   y�55       �� *HasReagent2+# [ .+�56   &   �� $��0,0,2,0q�6N   ��60   �� *KInp2Display+6N   �� 57   &   ��60   �� *Reaction.Inp2+�� 58   &   $��Bottom=�6M   $��2,-2,0,0q�6h   $��12(�6z   ��60   �� *Ind2Display+6z   59   #   �� 5<   "   $	�� + ��6,   y�67   �� *	ShowPlus2+# [ �� 5?   "   $ 
Horizontal=�6-   y�5@       �� *HasReagent3+# [ .+�5A   &   �� $��0,0,2,0q�6N   ��60   �� *KInp3Display+6N   �� 5B   &   ��60   �� *Reaction.Inp3+�� 5C   &   $��Bottom=�6M   $��2,-2,0,0q�6h   $��12(�6z   ��60   �� *Ind3Display+6z   5D   #   �� 5G   "   $�� → ��6,   $	��5,0q�67   �� 5J   "   $ 
Horizontal=�6-   .+�5K   &   �� $��0,0,2,0q�6N   ��60   �� *KProdDisplay+6N   �� 5L   &   ��60   �� *Reaction.Prod+5M   #   5N      5O      5Q      5S      �� 5T   
   -   5        $!��Создать модель��5T      $
��10 5q�6L   $��5��6[   $��
10,10,10,0q�6h   $��0��6|   $��40��6�   $	��200��6�   $��Right=�6�   �� 5U   
   -   5        $0��*Добавить новую реакцию��5U      $
��10 5q�6P   $��5��6_   $��	10,10,5,0q�6l   $��1��6   $��40��6�   $	��200��6�   $
��Left=�6�   5V      5W      