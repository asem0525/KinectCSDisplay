   M S B A M L   `   `   `  ����F  BKinectBoard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:Microsoft.Samples.Kinect.ControlsBasics'Microsoft.Samples.Kinect.ControlsBasics  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	   	   8    2Microsoft.Samples.Kinect.ControlsBasics.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   d `Microsoft.Kinect.Wpf.Controls, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e350k(http://schemas.microsoft.com/kinect/2014  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   Alocal5clr-namespace:Microsoft.Samples.Kinect.ControlsBasics  5   	   U QMaterialDesignThemes.Wpf, Version=2.1.0.657, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   3System&clr-namespace:System;assembly=mscorlib  5	   	   -   5          �Name$  KinectBoardWindow��5   	    � Title$ Kinect Display Board��5   	   $	��500��5   	   $	��700��5   	    � WindowState$ 	Maximized=�5   	    � WindowStyle$ SingleBorderWindow=�5   	   .��5      0  *Microsoft.Kinect.Wpf.Controls.KinectRegion  -   5          Name$ kinectRegion��5      .��5   
   � ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF292929��6#   � 5      $ 	#FF020202��6#    � Offset$ 1Q�65   5       	 � RowDefinitions	 5"      �� 5#      $
��Auto �6    �� 5$      $
��100* �6    �� 5%      $��* �6    .+�5(      � -   5        
 �Name$
 topBar��5(      $��0��6#   $��0,0,0,7q�60   
���   � � ColumnDefinitions 5)      �� 5*      $
��Auto �6'   �� 5+      $��* �6'   .+�5/      �� -   5         ��Name$ 
backButton��5/      $y�Hidden=�6-   $��0��6A      BackButtonStyle#��[  6Q   6z   � 51      $��0��6   $��1��6$   	 53      �� 54      $
��0.2* �6(   �� 55      $
��0.3* �6(    57      �� 58      $
��Auto �6+   �� 59      $��* �6+   �� 5:      $��* �6+   .+�5>      �� -   5         ��Name$ image��5>      $��0��6O   $��0��6\   $��clas.png��6l   $
��Left=�6~   $��20��6�   $��Bottom=�6�   �� 5?      -   5         ��Name$	 Sci��5?   >   $��1��6    $��0��6.   $	��Top=�6K   $��36(�6c   $��Segoe UI Black)�6q   $
��Left=�6�   ���	#DDFFFFFF$��0q�6�   $��Computer Science��6�   (� 5A      $��0��6   $��2��6+   $��1��6<   $	��150��6M   $	��150��6Z   .��5B      �� -   5        $��KinectStreamView��5B       $��UniformToFill=�6W   ��68   �� *ImageSource+6W   5C      �� 5E      -   5        $ image1��5E      $��20��6,   $��0��68   $��2��6E   $��Images/uiowa.png��6U   $��Bottom=�6o   $��Right=�6�   $��0,0,25,0q�6�   �� 5F      -	   5         ��Name$ DateTimeClock��5F   !    �� Orientation$ 
Horizontal=�68   $��1��6Q   $��2��6^   $��Right=�6n   $	��Top=�6�   $��0,0,20,0q�6�   .+�5G      �� ���#333333 �� CornerRadius$ 5��66   $��5q�6G    �� Padding$ 5q�6R   ._�5H      �� -
   5        $ 	clockText��5H   (   $��20(�6<   $��Segoe UI Black)�6J   ���	#DDFFFFFF$	��Top=�6}   �� 6�   $�00:00 pm��6�   6�   5I      �� 5J      ���#333333$ 5��66   $��5q�6G   $ 5q�6R   $	��Top=�6^   ._�5K      �� -   5        $ dateText��5K   (   $��20(�6:   $��Segoe UI Black)�6H   $��Center=�6d   ���	#DDFFFFFF$��0q�6�   �� 6�   $�
Mon, Jan 1��6�   ���	#FF3333336�   6�   5L      5N      5P      5Q      �� 5T      -   5         ��Name$ navigationRegion��5T   ;   $��1��6   $��2��6*   .��5V      � -   5        $
 kinectRegionGrid��5V      $��5,5,5,20q�61    5W      �� 5X      $
��0.4* �6+   �� 5Y      $
��0.6* �6+   	 5[      �� 5\      $	��15* �6(   �� 5]      $
��100* �6(   .+�5`      �� -   5        $ weatherPanel��5b   !   $��0��5`   !   $��0��5a   !   $ 
Horizontal=�5c   !   $��0,10,0,0q�6:   .+�5d      �� .+�5e      ��   HeaderStyle#��[ 6(   $
��Wrap=�6N   $��Forecast��6b   $	��Top=�6r   ���	#FFFFC000$��24(�6�   $��0q�6�   $��Center=�6�   �� 5f      -   5        $ wuLogo��5f   $   $-��'Images/wundergroundLogo_4c_rev_horz.jpg��65   $��25��6f   $	��Top=�6r   5g      �� 5h      -   5        $ weather1��5h   %   $
��10,0q�68   $��Center=�6F   .+�5i      �� -   5        $
 day1��5i   (    �� TextAlignment$ Center=�66   $��20(�6M   ���	#FFFFC000Today6y   �� 5j   "   -   5        $ weatherIcon1��5j   (   $
��40px��6>   $
��40px��6K   �� 5k   "   -   5        $ Temp1��5k   ,   $��20(�6;   $ Center=�6I   
�������6u   5l      �� 5m      $��Stretch=�6$   $��1��6@   $��2q�6J   
�������$	��0.5��6d   �� 5o      -   5        $ weather2��5o   %   $
��10,0q�68   .+�5p   "   �� -   5        $
 day2��5p   ,   $��20(�6:   $ Center=�6H   ���	#FFFFC0006x   �� 5q   "   -   5        $ weatherIcon2��5q   (   $
��40px��6>   $
��40px��6K   �� 5r   "   -   5        $ Temp2��5r   ,   $��20(�6;   $ Center=�6I   
�������6v   5s      �� 5t      -   5        $ weather3��5t   %   $
��10,0q�67   .+�5u      �� -   5        $
 day3��5u   (   $��20(�66   $ Center=�6E   ���	#FFFFC0006u   �� 5v   "   -   5        $ weatherIcon3��5v   (   $
��40px��6>   $
��40px��6K   �� 5w   "   -   5        $ Temp3��5w   ,   $��20(�6;   $ Center=�6I   
�������6u   5x      �� 5y      -   5        $ weather4��5y   )   $
��10,0q�6;   .+�5z      �� -   5        $
 day4��5z   (   $��20(�66   $ Center=�6D   ���	#FFFFC0006t   �� 5{   "   -   5        $ weatherIcon4��5{   (   $
��40px��6>   $
��40px��6K   �� 5|   "   -   5        $ Temp4��5|   ,   $��20(�6;   $ Center=�6I   
�������6u   5}      5~      �� 5�      -    5         ��Name$ scrollViewer��5�   #   $��1��5�   #   $��0��5�   #   $��Hidden=�5�   #   $��Disabled=�5�   #      IsScrollInertiaEnabled . .��5�      �� -!   5         ��Name$ itemsControl��5�   4   $��0��6'     Kinect250x250ItemTemplate#��[ 6J   $
��0,10q�6�   ��5�      �� 5�   "   .R�5�   &   � $��Center=�60    � Orientation$ Vertical=�6K   $��	75,0,75,0q�6b   5�   #   5�      5�      � 5�      -"   5        $
 RightGridPanel��5�      $��1��5�      $��0��5�      $��2��5�      $��10,0,0,0q�5�      	 5�      �� 5�      $��6* �6,   �� 5�      $��4* �6,   .+�5�      � $��0��6   	 5�      �� 5�   "   $
��Auto �60   �� 5�   "   $��* �60   .+�5�      �� -#   5        $ NewsAndAnnouncment��5�   Z   $��0��6(   #��[ 65   $
��Wrap=�6v   $��Upcoming News & Events��6�   $	��Top=�6�   $��Stretch=�6�   ���	#FFFFC0006�   �� 5�      -$   5        $ Slides��5�   8   $��1��6+   ��5�   "   �� 5�   &    �� 
TargetType" ��6,   .�5�   *   ��  �� Property � �� �� Value$ Disabled=�6Z   �� 5�   *    � ��$ Disabled=�6X    �� Triggers 5�   *   �� 5�   .    �� Binding 6:   �� *	IsChecked+ �� ElementName$ chkActualSize�� �� Value True6s   .a�5�   2   ��  � ��$
 Auto=�6b   �� 5�   2    � ��$
 Auto=�6`   5�   /   5�   '   .��5�   "   �� -%   5        $ 
SlideImage��5�   (   $��Uniform=�6[   ��6<   �� *SlideSource+6[   5�      �� 5�      -&   5        $ EventsSlide��5�   +   $��Disabled=�5�   +   $
��Auto=�5�   +   $��1��5�   +   .��5�   "   �� -'   5          ��Name$  csEventsList��5�   +   $��Stretch=�5�   '   ! �� SelectionMode$! Single=�5�   '   $��	10,0,10,5q�5�   (   $��Disabled=�5�   '   $
��Auto=�5�   '   ��5�   '   Z� ��5�   '   Z� ��5�   &   �� 5�   *   " ��60   .�5�   .   ��  � ��$ Stretch=�6[   �� 5�   .    � ��
 ���� 6K   �� 5�   .    � �� 5�   2   �� 5�   6   " �� 
TargetType"" ��6F   .R�5�   :   �� 5�   7   5�   /   5�   +   ��5�   &   �� 5�   *   .R�5�   .   �� $��Stretch=�69   .+�5�   2   �� $ csEventTitle��6<   $��20(�6p   $
��Wrap=�6~   
���������6R   �� *csEventTitle+6�   �� 5�   2   $ csEventTime��6<   $��18(�6n   $��Italic$�6|   ���	#FFaaaaaa��6Q   �� *csEventTime+6�   �� 5�   2   $ csEventLocation��6<   $��18(�6v   ���	#FFaaaaaa��6U   �� *csEventLocation+6�   5�   /   5�   +   5�   #   5�      5�      �� 5�      $��1��6$   $	��Top=�61   
�������6^   �� 5�      $��1��6%   $��Stretch=�62   .+�5�      �� -(   5        $ busScheduleTitle��5�   M   #��[ 6(   $��1��6g   ���	#FFFFC000Upcoming Buses6�   &   System.Windows.Controls.DataGrid  5�      -)   5        #  Name$# BusGrid��5�   '   $   
IsReadOnly$ . %   AutoGenerateColumns% .  &   SelectionMode$& Single=�6~   '   SelectionUnit$
' Cell=�6�   ��6J   �� ��6�   Z� (   Columns( 5�   "   0  *System.Windows.Controls.DataGridTextColumn  5�   &   ,  &System.Windows.Controls.DataGridColumn)   Header$) 	Stop Name��69   *   FontSize$* 16Q�6i   1  +System.Windows.Controls.DataGridBoundColumn+   Binding+ 6L   �� *
stopname+6i     5�   &   $) 
Route Name��69   $* 16Q�6k   + 6M   �� *	routename+6k     5�   &   $
) Time��69   $* 16Q�6c   + 6G   �� *	minutes+6c   ,   ColumnHeaderStyle, 5�   "   �� 5�   &   =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6,   .�5�   *   ��  � ��
 �����6G   �� 5�   *    � ��
 ���� 6G   5�   '   -   RowStyle- 5�   "   �� 5�   &   )  #System.Windows.Controls.DataGridRow"  6,   .�5�   *   ��  � �� 6G   �� *color+5�   '   . � 	Resources. 5�   "   ��%a   '        �� 5�   &   "  6,   .�5�   *   ��  �@J�IsHitTestVisible .  6M   5�   '   5�   #   5�      5�      5�      5�      5�      �� 5�      -*   5        $ 	bottomBar��5�   �   $ 
Horizontal=�6   $��26��62   $��Bottom=�6>   ���	#FF020202$��Stretch=�6r   $��2��6�   $��1��6�   . 5�      / ��Name  sb0 g� RepeatBehavior1 �� 
TargetName2 �� TargetProperty	3 W� To4 g� Duration5 W� From6 �� Source��%�   &	       �� 5�      $/ sb��6,   $0 Forever��68   .��5      W� $1 	translate��6*   $2 X�5  *   $3 -3500X�5  *   $4 0:0:59D�5  *   5 5  *   �� #6 Z��5  *   5     5     .+�5     �� -+   5        $ bottomBarText��5     $��Center=�64   $��20(�6O   $��Segoe UI Black)�6]   ���	#DDFFFFFFz�5     T� 5	     -,   5        	translate��5	  -   ��Welcome to MacLean Hall! ~~~~~ 欢迎来到 MacLean Hall ~~~~~ ¡Bienvenido a MacLean Hall! ~~~~~ आपका स्वागत है MacLean Hall ~~~~~ To use this board, place your hand in the air and find the hand cursor. ~~~~~ To click on a menu, line the cursor on the button and press it in the air. ~~~~~ The back button is in the top left corner.5     5     F� 5     5����    ���� Zb ����2G[��K�/`@��2G[��K�/`@	$��2.495��6Q   $��0,0,-0.5,8.488q�6`   7 �� Stretch$
7 Fill=�6x   ���	#FFFFC000$��Bottom=�6�   $��5��6�   5     5     5     