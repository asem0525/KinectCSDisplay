   M S B A M L   `   `   `  ����F  BKinectBoard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullm;clr-namespace:Microsoft.Samples.Kinect.ControlsBasics.Pages-Microsoft.Samples.Kinect.ControlsBasics.Pages  5      Dclr-namespace:Microsoft.Samples.Kinect.ControlsBasics.Helper_Classes6Microsoft.Samples.Kinect.ControlsBasics.Helper_Classes  5      >    8Microsoft.Samples.Kinect.ControlsBasics.Pages.PeoplePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Mlocal;clr-namespace:Microsoft.Samples.Kinect.ControlsBasics.Pages     5      U QMaterialDesignThemes.Wpf, Version=2.1.0.657, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      d `Microsoft.Kinect.Wpf.Controls, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e350k(http://schemas.microsoft.com/kinect/2014  5	      J FWpfAnimatedGif, Version=1.4.14.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5
      RloadingDclr-namespace:Microsoft.Samples.Kinect.ControlsBasics.Helper_Classes  5      $��10,10,10,10q�5      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��    HeaderStyle#��[  6   $��	UltraBold"�69   $��0��6P   $��0��6]   $��	0 10 0 10q�6m   $��People��6�   $��36(�6�   ���	#FFEEEEEE6�   K   EMicrosoft.Samples.Kinect.ControlsBasics.Helper_Classes.LoadingControl  5   
   -   5          Name$ Loading��5   K   $��Center=�6!   $��1��6>   6K   �� 5   
   -   5         ��Name$ scrollViewer��5      $��1��5      $
��Auto=�5      $��Disabled=�5      0  *Microsoft.Kinect.Wpf.Controls.KinectRegion   IsScrollInertiaEnabled . .��5      &   System.Windows.Controls.DataGrid  -   5          Name$ 
PeopleGrid��5         AlternatingRowBackground �	#FF424242 �� AlternationCount$ 2��5   <   ��. $
��Auto=�5         
IsReadOnly .    AutoGenerateColumns .  	   EnableRowVirtualization	 .  !
   EnableColumnVirtualization
 .  ��5    )   �� ��6]   Z�    ColumnHeaderStyle 5!      �� 5"       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5#      ��  �� Property � �� �� Value
 �����67   �� 5$       � ��
 ���� 67   5%         	CellStyle 5'      �� 5(      *  $System.Windows.Controls.DataGridCell"  6   .�5)      ��  � ��
 �����67   �� 5*       � ��$
 17.5(�65   5+       � 	Resources 5-      ��%T   '��       �� 5.      " ��6   .�5/      ��  � ��$
 Wrap=�69   50      51         Columns 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6)      FontSize$ 18Q�6P      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6^      
FontWeight$
 Bold"�6k      
Foreground
 �����1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 67   �� *Name+ 	  ElementStyle 54      �� 55      .�56   "   ��  � ��$
 Wrap=�6K   57      59        5:      $ Office��6)   $ 17Q�6T      	FontStyle$ Italic$�6b   
 ����� 69   �� *Office+ 5;      �� 5<      .�5=   "   ��  � ��$
 Wrap=�6K   5>      5@        5A      $ Hours��6)   $ 17Q�6R   
 ����� 68   �� *Hours+ 5B      �� 5C      .�5D   "   ��  � ��$
 Wrap=�6K   5E      5G        5H      $ Email��6)   $ 17Q�6R   
 ����� 68   �� *Email+ 5I      �� 5J      .�5K   "   ��  � ��$
 Wrap=�6K   5L      5N      5P      5Q      5R      5S      