   M S B A M L   `   `   `  ����  ClickTimerGame    Klikacka.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Klikací hra��5   	   $	��720��6   $
��1280��6*   .��5      � .+�5   
   � -   5        $��MainMenu��5      $y�Visible=�6   ��5      �� 5       �� ImageSource$ Images/pozadicko.jpg��6    j� Stretch$
 Fill=�6@    j� TileMode$
 None=�6O    �� Opacity$
 0.85Q�6_   .+�5      �� $��Fufové, třeste se!��6   $	��120(�62   $
��Bold"�6A   $��Center=�5      $	��Top=�65   $��	0,129,0,0q�6M   $��Arial Black)�6`   �� 5      -   5        $��Start��5      $	��600��6%   $	��120��61   $��Center=�5      $	��Top=�62   $��64(�5   /   $��Arial Black)�6=    � Cursor$
 Hand��6V   $��	0,352,0,0q�6d    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5      $
��left=�6   $	��Top=�64   $��150,500,0,0q�6L   .+�5      �� $��Klikat��6   $��40(�6*   $
��Bold"�68   �� 5      $��Images/fufar.png��6   $	��100��62   $	��100��6>   5      �� 5      $��Right=�6   $	��Top=�65   $��0,500,150,0q�6M   .+�5       �� $��Neklikat��6   $��40(�6,   $
��Bold"�6:   $��Center=�6L   �� 5!      $��Images/mareckos.png��6   $	��100��65   $	��100��6A   $��Center=�6N   5"      5$      � 5&   
   -   5        $��
GameScreen��5&      $y�	Collapsed=�6!   .+�5'      �� -   5        $��
GameCanvas��5'      
����ح�6'   �� 5(      -   5        $��	ScoreText��5(      $��24(�5)      $
��Bold"�6&   $
��Left=�5*      $	��Top=�63   $��10q�5+      $��	Skóre: 0��6$   5,      5-      5.      