   M S B A M L   `   `   `  ����F  Bdbwpfprject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:dbwpfprjectdbwpfprject  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5          dbwpfprject.DBWPF   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:dbwpfprject     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5        N� Title$  DBWPF��5
      .��5      � 
���   �.+�5   
   #  LiveCharts.Wpf.CartesianChart  $��
0,168,10,0q�6   &   LiveCharts.Wpf.Charts.Base.Chart   Series 5        LiveCharts.Wpf.LineSeries    LiveCharts.Wpf.Series   Title$ 	CPU Usage��6>      Values 6!   �� *	CpuValues+6>     5      $ Memory Usage��6>    6!   �� *	MemValues+6>     5      $ 
Disk Usage��6?    6!   �� *
DiskValues+6?   5        LiveCharts.Wpf.Gauge  5   
   -   5          Name$ cpugauge��5      $��0��6&   $��0��63   $��10,20,606,305q�6C      Uses360Mode .    From$ 0Q�5      	   To$	 100Q�6   
�������   GaugeBackground 5      �� 5      	 �� Color$	 White��6"   
 �� Opacity$	
 0.1Q�60   5      �� 5   
   -   5         ��Name$ startbut��5      $��START��6#   $
��Left=�63   $��
642,10,0,0q�6N   $	��Top=�6b   $��48��6z   $	��130��6�   $��16(�6�   $
��Bold"�6�    � 	Resources 5       �� 
TargetType �� Property �� Value��%_   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 15��65   5      5       5!        5"   
   -   5        $ memguage��5"      $ 0Q�5#      $	 100Q�6      LabelsVisibility$ 	Collapsed=�5$      $��206,10,405,305q�61   ���	#FFEFEFEF   GaugeActiveFill �	#FF98C1D9 5%      �� 5&      $	 White��6"   $	
 0.1Q�60   5(        5)   
   -   5        $ 	diskguage��5)      $ 0Q�5*      $	 100Q�6   $ 	Collapsed=�5+      $��415,10,196,305q�6*   ���	#FFEFEFEF �	#FFEE6C4D 5,      �� 5-      $	 White��6"   $	
 0.1Q�60   5/      �� 50   
   $��	디스크��6   $
��Left=�6   $��26��69   $��
64,136,0,0q�6E   $	��Top=�6Y   $��62��6q   ���	#FFEFEFEF$
��Bold"�6�   $��16(�6�   �� 51   
   $��	메모리��6   $
��Left=�6   $��26��69   $��261,135,0,0q�6E   $	��Top=�6Z   $��66��6r   ���	#FFEFEFEF$
��Bold"�6�   $��16(�6�   �� 52   
   $	��CPU��6   $
��Left=�6   $��31��69   $��478,131,0,0q�6E   $	��Top=�6Z   $��48��6r   ���	#FFEFEFEF$
��Bold"�6�   $��16(�6�   �� 53   
   -   5        $ pastlog��53      $��	조   회��6"   $
��Left=�62   $��
642,82,0,0q�6M   $	��Top=�6a   $��48��6y   $	��130��6�   $��16(�6�   $
��Bold"�6�    54      ��%_   '��       �� 55      " ��6   .�56      ��  �@��CornerRadius$ 15��65   57      58      59      5:      5;      