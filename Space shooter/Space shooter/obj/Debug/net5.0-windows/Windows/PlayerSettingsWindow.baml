   M S B A M L   `   `   `  ����H  DSpace shooter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Space_shooter.WindowsSpace_shooter.Windows  5   	   0    *Space_shooter.Windows.PlayerSettingsWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Space_shooter.Windows     5   	   -   5          � Title$  PlayerSettingsWindow��5   	   $	��735��6&   $	��550��63    � AllowsTransparency$
 True��6?    � Cursor$ Images/Cursor_arrow.cur��6Y   
������  � WindowStartupLocation$ CenterScreen=�5	   "    � 
ResizeMode$ NoResize=�6G    � WindowStyle$
 None=�6]    � 	Resources 5       �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� Triggers P� Property P� Value��%�  '��       '	� �    �� 5   
   " ��6   .�5      ��  � ��$	 40(�6)   �� 5       � ��$	 10q�6'   �� 5       � ��$	 60��6*   �� 5       � ��$		 100��6&   �� 5       � ��
	 ���� 6+   �� 5       � ��
	 ���� 6,   �� 5       � ��
	 �����6+   �� 5       �@�Cursor$	 Images/Cursor_grab.cur��6'   �� 5       � ��	 5      �� 5      "
 ��6&   .R�5      �� #��z��6!   #��z��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�5       5       5      P� 5       �@J�IsMouseOver$
 True��5       . �5!   "   ��  � ��
	 � ���5"      5#      5%      5'      5(      �� 5)   
   " 	�6   .�5*      ��  � ��$	 20(�6)   �� 5+       � ��$	 30��6'   �� 5,       � ��$	 5q�6'   �� 5-       � ��
	 �����6+   �� 5.       � ��$	 Center=�69   �� 5/       �@�Cursor$	 Images/Cursor_grab.cur��6'   50      51      .��53      � -   5         �Name$ 
PlayerGrid��53      ��54   
   �� 55       �� ImageSource$&  Images/Menu_Background_light.gif��6    � RowDefinitions 57   
   �� 58      $
��327* �6   �� 59      $	��68* �6   �� 5:      $	��57* �6   �� 5;      $
��283* �6   .+�5=   
   �� $��New Game��6   $��56(�6#   $��Center=�61   $��Center=�6S   
��� ���$��	Agency FB)�6�   $��
0,78,0,172q�6�   $	��194��6�   �� 5>   
   $��1��6   $��Center=�6"   $	��350��6?   $��0,0,0,25q�6K   $��2��6]   .+�5?      �� $��
Difficulty��6   
��� ���$��Center=�6=   $��35(�6Z   $��0,0,0,10q�6h   $	��120��6z   $
��Bold"�6�   $��	Agency FB)�6�   �� 5@      -   5         ��Name$ sp_difficulty��5@       �� Orientation$ 
Horizontal=�60   $��45��6I   .+�5A      	� -   5         	�Name$ rb_easy��5A      $
��Easy��6/   	� 5B      -   5        $ 	rb_medium��5B      $��Medium��61    d� 	IsChecked$
 TrueY�6B   	� 5C      -   5        $ rb_hard��5C      $
��Hard��6/   	� 5D      -   5        $ 	rb_custom��5D      $��Custom��61   6B   5E      5F      �� 5G   
   -   5         ��Name$ tb_playername��5G      $��Type here your name��6)   $��2��6D   $��15(�6�   $	��Top=�6�   $��Center=�6�   $��25��6�   $	��250��6�    J� Opacity$
 0.65Q�6�   $��0,43,0,0q�6  $��2��6  �� 5H   
   -	   5        $��Start��5H      $��3��6=   $��	Agency FB)�6J   $��225,24,225,207q�6a   �� 5I   
   -
   5        $
��Back��5I      $��3��6;   $��	Agency FB)�6I   $��225,188,225,45q�6`   5J      5K      