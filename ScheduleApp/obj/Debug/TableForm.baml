   M S B A M L   `   `   `  ����F  BScheduleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ScheduleAppScheduleApp  5   	       ScheduleApp.TableForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ScheduleApp     5   	     � Title$  DaySchedule��5   	   $��635.5��6   $	��568��6,   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5        $��dataGrid��5
   A      
IsReadOnly .    AutoGenerateColumns .  $
��Left=�6Q   $	��406��6l   $	��Top=�6y   $	��560��6�   �� 5   
   -   5        $��Добавить��5      $
��Left=�6$   $��
55,574,0,0q�6?   $	��Top=�6S   $��76��6k   6v   �� 5   
   -   5        $��lessonId��5      $
��Left=�6"   $��22��6=    �� TextWrapping$
 Wrap=�6I   $	��Top=�6]   $	��120��6u   $��138,424,0,0q�6�   �� 5   
   -   5        $��lessonL��5      $��ID предмета��6   $
��Left=�65   $��
10,420,0,0q�6P   $	��Top=�6d   �� 5   
   -   5        $��dayId��5      $
��Left=�6   $��22��6:   $
 Wrap=�6F   $	��Top=�6Z   $	��120��6r   $��138,466,0,0q�6~   �� 5   
   -   5        $
��dayL��5      $��ID дня недели��6   $
��Left=�64   $��
10,462,0,0q�6O   $	��Top=�6c   �� 5   
   -   5        $��classId��5      $
��Left=�6!   $��22��6<   $
 Wrap=�6H   $	��Top=�6\   $	��120��6t   $��138,503,0,0q�6�   �� 5   
   -   5        $��classL��5      $��ID класса��6   $
��Left=�62   $��
10,499,0,0q�6M   $	��Top=�6a   $��87��6y   �� 5   
   -	   5        $��Удалить��5      $
��Left=�6#   $��332,462,0,0q�6>   $	��Top=�6S   $��98��6k   $��41��6v   6�   �� 5   
   -
   5         ��Name$ id��5      $
��Left=�6   $��22��69   $
 Wrap=�6E   $	��Top=�6Y   $	��120��6q   $��138,539,0,0q�6}   �� 5   
   -   5        $	��idL��5      $��ID расписания��6   $
��Left=�63   $��
10,535,0,0q�6N   $	��Top=�6b   $��91��6z   �� 5   
   -   5        $��printBtn��5      $��Печать��6!   $y�Hidden=�62   $
��Left=�6F   $	��Top=�6a   $��98��6y   $��332,528,0,0q�6�   $��33��6�   6�   5      5      