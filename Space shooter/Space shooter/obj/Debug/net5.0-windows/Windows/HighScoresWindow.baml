   M S B A M L   `   `   `  ����H  DSpace shooter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Space_shooter.WindowsSpace_shooter.Windows  5   	   ,    &Space_shooter.Windows.HighScoresWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Space_shooter.Windows     5   	   -   5          � Title$  HighScoresWindow��5      $	��735��6!   $	��550��6.    � AllowsTransparency$
 True��6:    � Cursor$ Images/Cursor_arrow.cur��6T   
������  � WindowStartupLocation$ CenterScreen=�5	   9    � WindowStyle$
 None=�6^    � 
ResizeMode$ NoResize=�6�    � 	Resources 5
       �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� Triggers P� Property P� Value   TransparentBackground �� Color �� Opacity��%   '��       &	  �    �� 5   
   " ��6   .�5      ��  � ��$	 50(�6)   �� 5       � ��$	 0,0,0,45q�6'   �� 5       � ��$	 60��6*   �� 5       � ��$		 100��6&   �� 5       � ��
	 ���� 6+   �� 5       �@�Cursor$
	 Hand��6'   �� 5       � ��
	 ���� 6,   �� 5       � ��
	 �����6+   �� 5       �@�Cursor$	 Images/Cursor_grab.cur��6'   �� 5       � ��	 5      �� 5      "
 ��6&   .R�5      �� #��z��6!   #��z��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�5       5       5      P� 5       �@J�IsMouseOver$
 True��5       . �5!   "   ��  � ��
	 � ���5"      5#      5%      5'      5(      �� 5)   
   $ Black��68   $	 0.5Q�6F   5*      .��5,      � ��5-   
   �� 5.       �� ImageSource$&  Images/Menu_Background_light.gif��6    � RowDefinitions 50   
   �� 51      $��6* �6   �� 52      $	��25* �6   �� 53      $	��115 �6    � ColumnDefinitions 55   
   �� 56      $	��15* �6   �� 57      $	��50* �6   �� 58      $	��15* �6   .+�5:   
   �� $��
Highscores��6   
��� ���$��	Agency FB)�69   $��56(�6P   $��1��6^   $��Bottom=�6n   $��Center=�6�   $��Center=�6�   $	��120��6�   �� 5;   
   $��Hidden=�6   $��Hidden=�6>   $��1��6c   $��3��6p   $��103,20,103,20q�6�   .��5<      �� -   5         ��Name$ 	sp_scores��5<      #��[  6,   5=      �� 5>   
   -   5        $
��Back��5>      $��2��6    $��1��6-   $��	Agency FB)�6Y   5?      5@      