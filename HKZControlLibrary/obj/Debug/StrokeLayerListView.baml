   M S B A M L   `   `   `  ����L  HHKZControlLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:HKZControlLibraryHKZControlLibrary  5      +    %HKZControlLibrary.StrokeLayerListView   5   
   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:HKZControlLibrary     5        � 	Resources  5
       �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels � Cursor��%  '��       �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ Bd��6!   
������� . $
 Hand��6[   ._�5      �� #��z��6/   # z 5   /   #��z��5   /   5      5      5      5      5      ��5      �� 5   
   .R�5      �  �Name$ ItemBd��6   $	��150��6#   $	��110��6/   $��5q�6<   .+�5      �� $ itemB��6   $��1q�6(   ���	#FFDDDDDD._�5      � $	��146��6   $	��106��6'   $��2 2 2 2q�64   .+�5      �� 	 ��Name$	 mImage��6    $	��150��60   $	��110��6<   $
��Fill=�6I   6Z   � 5      $��25��6   $��25��6*   ���	#44000000$	��Top=�6M   $
��Left=�6e   .+�5       �� $��16(�6=   $
��Auto��6K   $
��Auto��6X   $��CharacterEllipsis=�5!   0   
�������$��Center=�6d   $��Center=�6�   ��5    (   �� *FId+5!   �   5"      5#      5$      5%      
 �� Triggers
 5&      �� 5'       �� Value$
 true��6�    �� Binding 6   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" �� �� Path$ 
IsSelected�.a�5(      ��  �� 
TargetName$ itemB��6    � ��
 �  ��6G   5)      �� 5*      $
 True��6�    6   ��  �� $ FindAncestor=�" ��$ IsMouseOver�.a�5+      �� $ itemB��6    � ��
 � ���6G   5,      �� 5-      $ false��68    6   �� *FAdd+.a�5.      �� $ mImage��6    6C   �� *	FBitmap+ � ��5/      �� 50      $
 true��68    6   �� *FAdd+.a�51      �� $ mImage��6    � ��$ res\add.png��6C   52      54      56      