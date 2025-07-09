// app/esewa-webview.tsx
import { useLocalSearchParams, useRouter } from "expo-router";
import { WebView } from "react-native-webview";
import { View, ActivityIndicator, Alert } from "react-native";
import uuid from "react-native-uuid";
import axios from "axios";

export default function EsewaWebview() {
  const { amount } = useLocalSearchParams<{ amount: string }>();
  const router = useRouter();
  const pid = uuid.v4();

  const successUrl = `https://dummy.success.com?pid=${pid}&amt=${amount}`;
  const failureUrl = `https://dummy.failure.com`;

  const html = `
    <html>
      <body>
        <form id="esewa_form" action="https://rc-epay.esewa.com.np/api/epay/main/v2/form" method="POST">
          <input type="hidden" name="tAmt" value="${amount}" />
          <input type="hidden" name="amt" value="${amount}" />
          <input type="hidden" name="txAmt" value="0" />
          <input type="hidden" name="psc" value="0" />
          <input type="hidden" name="pdc" value="0" />
          <input type="hidden" name="scd" value="EPAYTEST" />
          <input type="hidden" name="pid" value="${pid}" />
          <input type="hidden" name="su" value="${successUrl}" />
          <input type="hidden" name="fu" value="${failureUrl}" />
        </form>
        <script>document.getElementById("esewa_form").submit();</script>
      </body>
    </html>
  `;

  const handleNavigation = async (event: any) => {
    const { url } = event;

    if (url.includes("dummy.success.com")) {
      const params = new URLSearchParams(url.split("?")[1]);
      const pid = params.get("pid");
      const amt = params.get("amt");

      try {
        const res = await axios.post("https://23e3606805ca.ngrok-free.app/api/Payment/add", {
          pid,
          amount: amt,
        });

        if (res.status === 200) {
          router.push("/payments/success"); // ✅ navigate to internal screen
        } else {
          router.push("/payments/failure");
        }
      } catch (err) {
        Alert.alert("Error", "Payment verification failed.");
        router.push("/payments/failure");
      }
    }

    if (url.includes("dummy.failure.com")) {
      router.push("/payments/failure"); // ✅ navigate to internal screen
    }
  };

  return (
    <View style={{ flex: 1 }}>
      <WebView
        source={{ html }}
        javaScriptEnabled
        onNavigationStateChange={handleNavigation}
        startInLoadingState
        renderLoading={() => <ActivityIndicator size="large" />}
      />
    </View>
  );
}
