   M S B A M L   `   `   `  ���� F  BKinectBoard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullm;clr-namespace:Microsoft.Samples.Kinect.ControlsBasics.Pages-Microsoft.Samples.Kinect.ControlsBasics.Pages  <    6Microsoft.Samples.Kinect.ControlsBasics.Pages.CSEvents   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Mlocal;clr-namespace:Microsoft.Samples.Kinect.ControlsBasics.Pages     U QMaterialDesignThemes.Wpf, Version=2.1.0.657, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  d `Microsoft.Kinect.Wpf.Controls, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e350k(http://schemas.microsoft.com/kinect/2014  $��10,10,10,10q�.��� -     �Name$  MainEventsGrid�� � DataContext ��    CSEventSampleData �� Source# [   � RowDefinitions �� $
��Auto ��� $��* � � ColumnDefinitions �� $
��0.5* ��� $
��0.5* �.+���   HeaderStyle#��[ $��	UltraBold"�$��0��$��0��$��0,0,0,5q�$��News & Events��$��36(����	#FFEEEEEE� -   $  
EventsGrid��$��1�� �� $
��Auto ��� $
��0.7* ��� $
��0.3* �.+��� -    ��Name$ eventsTitle��$��0��#��[ $��10,0,0,0q����	#FFFFC000Department Events�� -    ��Name$ scrollViewer��$��1��$��Disabled=�$
��Auto=�0  *Microsoft.Kinect.Wpf.Controls.KinectRegion   IsScrollInertiaEnabled . $��10,0,0,0q�.��&   System.Windows.Controls.DataGrid  -     Name$ EventsDataGrid��	   AlternatingRowBackground	 �	#FF424242
 �� AlternationCount$
 2��   AutoGenerateColumns .     SelectionMode$ Single=�   SelectionUnit$
 Cell=�$   HorizontalScrollBarVisibility$ Disabled=�"   VerticalScrollBarVisibility$
 Auto=�   
IsReadOnly . ���� ��Z�    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Event��   FontSize$ 18Q�   Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.4*    
FontWeight$
 Bold"�   
Foreground
 �����1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *csEventTitle+   ElementStyle �� .���  �� Property � �� �� Value$
 Wrap=�  $
 Time��$ 18Q�$
 0.2*    	FontStyle$ Italic$�
 ����� �� *csEventTime+ �� .���  � ��$
 Wrap=�  $ Location��$ 18Q�$
 0.3* 
 ����� �� *csEventLocation+ �� .���  � ��$
 Wrap=�   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��
 �������  � ��
 ����    RowStyle �� )  #System.Windows.Controls.DataGridRow"  .���  � �� �� *color+ � 	Resources ��%<   '        �� "  .���  �@J�IsHitTestVisible .  �� $��2��.+��� -   $ 	newsTitle��#��[ $��Department News��$��10,0,0,0q����	#FFFFC000�� -     ��Name$  csCard��$
��10,5q�.+��� -   $ csNewsTitle��$��20(�$
��Wrap=�
��������� -	   $ 
csNewsTime��$��18(�$��Italic$�
�������No News at this time...�� -
   $ csNewsLocation��$��18(�
�������� -   $  NewsGrid��$��1��$��1�� �� $
��Auto ��� $��* �.+��� -   $ generalNewsTitle��$��0��#��[ $��Today's Headlines��$��10,0,0,0q����	#FFFFC000� $��1�� �� $
��0.6* ��� $
��0.4* �.+��� -   ! ��Name$! genNewsList��$��0��$��Stretch=�" �� SelectionMode$" Single=�$��	10,0,10,5q�$��Disabled=�$
��Auto=���Z� ��Z� ���� " ��.���  � ��$ Stretch=���  � ��
 ���� ��  � �� �� # �� 
TargetType"# ��.R��� ���� .R��� $  generalNewsCard��$
��10,0q�.+��� $ genNewsTitle��$��20(�$
��Wrap=�
����������� *	NewsTitle+�� $ genNewsDescription��$��18(�$
��Wrap=����	#FFaaaaaa���� *NewsDescription+�� $ genNewsDate��$��18(�$
��Wrap=�$��Italic$����	#FFaaaaaa���� *NewsPublicationDate+�� $��1��.+��� $ �� Orientation$$ 
Horizontal=�.+�	� -   % 	� 	GroupName$% NewsButtonGroup��$��Stretch=�$��AP Top News��  RadioButtonStyle#��[ 	� -   $% NewsButtonGroup��$��Stretch=�$��CNET Tech News��#��[ �� $$ 
Horizontal=�.+�	� -   $% NewsButtonGroup��$��Stretch=�$��BBC World News��#��[ 	� -   $% NewsButtonGroup��$��Stretch=�$��	CLAS News��#��[ 