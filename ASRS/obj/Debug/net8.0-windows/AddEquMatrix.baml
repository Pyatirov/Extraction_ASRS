   M S B A M L   `   `   `  ����?  ;ASRS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ASRSASRS  5   	       ASRS.AddEquMatrix   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:ASRS     5   	     � Title$-  'Компонентная матрица��5   	   $	��450��6&   $	��800��63   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��0.5* �6   �� 5      $��2* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5      �� $)��#Число базовых форм:��6   �� 5      -   5         ��Name$ txtBaseCount��5      $��50��6,   �� 5      -   5        $��Создать��5      6'   5       �� 5!   
   -   5         ��Name$ baseNamesPanel��5!      $��1��6-   $ 
Horizontal=�6:   �� 5$   
   $��2��6   $ 
Horizontal=�6"   $��5q�6;   .+�5%      �� $3��-Число образующихся форм:��6   �� 5&      -   5        $ resultCount��5&      $��50��6+   �� 5'      -   5        $��Создать��5'      6'   5(      �� 5)   
   -   5        $ formingNamesPanel��5)      $��3��60   $ 
Horizontal=�6=   �� 5*   
   -   5        $#��Создать матрицу��5*      $	��150��6I   $��30��6U   $��Center=�6a   $��2��6~   $��2��6�   �� 5+   
   $��Сохранить��6   $��5,5,5,5q�6%   $
��10 2q�66   $��5��6E   $��3��6R   �� 5.   
   $��4��6   $
��Auto=�6$   $��3��6I   .��5/      �� -   5        $ panelColumnHeaders��5/      $ 
Horizontal=�65   $	��5 0q�6N   50      &   System.Windows.Controls.DataGrid  53   
   -	   5          Name$ dataGrid��53      $��5��6%   $��5,5,5,5q�62   $��3��54         AutoGenerateColumns$ False��55         CanUserAddRows$ False��56      	   HeadersVisibility$	 Column=�57      $
   HorizontalScrollBarVisibility$

 Auto=�58      59      �� 5<   
   $��6��6   $��3��6!   .@�5=      �� -
   5         ��Name$ 	txtStatus��5=      5>      �� 5@   
   -   5        $��Применить��5@      $	��150��5A   	   $��30��6   $��Center=�6!   $��2��5B   	   $��2��6   5C      5D      