import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";
import * as Notifications from "expo-notifications";
import * as Device from "expo-device";
import { Platform } from "react-native";
import AsyncStorage from "@react-native-async-storage/async-storage";

let connection: HubConnection | null = null;

export async function registerForPushNotificationsAsync() {
  if (!Device.isDevice) {
    alert("Must use physical device for notifications");
    return;
  }

  const { status: existingStatus } = await Notifications.getPermissionsAsync();
  let finalStatus = existingStatus;

  if (existingStatus !== "granted") {
    const { status } = await Notifications.requestPermissionsAsync();
    finalStatus = status;
  }

  if (finalStatus !== "granted") {
    alert("Permission not granted");
    return;
  }

  if (Platform.OS === "android") {
    await Notifications.setNotificationChannelAsync("default", {
      name: "default",
      importance: Notifications.AndroidImportance.MAX,
      vibrationPattern: [0, 250, 250, 250],
      lightColor: "#FF231F7C",
    });
  }
}

export const startSignalRConnection = async () => {
  if (connection && connection.state === "Connected") return;

  const token = await AsyncStorage.getItem("token");

  connection = new HubConnectionBuilder()
    .withUrl("https://9ba67ed405da.ngrok-free.app/notificationhub", {
      accessTokenFactory: () => token || "",
    })
    .withAutomaticReconnect()
    .build();

  connection.on("ReceiveNotification", async (title: string, message: string) => {
    console.log("📩 SignalR notification:", title, message);

    await Notifications.scheduleNotificationAsync({
      content: {
         title: "Manual Test",
    body: "Notification works locally",
        sound: true,
      },
      trigger: null,
    });
  });

  try {
    await connection.start();
    console.log("✅ SignalR connected");
  } catch (err) {
    console.error("❌ SignalR connection error:", err);
  }
};

export const stopSignalRConnection = async () => {
  if (connection) {
    await connection.stop();
    connection = null;
    console.log("🛑 SignalR disconnected");
  }
};
