// utils/triggerLocalNotification.ts
import * as Notifications from 'expo-notifications';

export async function triggerLocalNotification(title: string, body: string) {
  await Notifications.scheduleNotificationAsync({
    content: {
      title,
      body,
      sound: true,
    },
    trigger: {
      type: 'timeInterval',
      seconds: 1,
      repeats: false,
    } as unknown as Notifications.NotificationTriggerInput,
  });
}
