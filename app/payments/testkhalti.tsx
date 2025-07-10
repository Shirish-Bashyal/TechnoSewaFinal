// app/khalti-payment.tsx
import React, { useRef } from "react";
import { View, ActivityIndicator, Alert } from "react-native";
import { WebView } from "react-native-webview";
import axios from "axios";
import { useRouter } from "expo-router";

export default function KhaltiPayment() {
  const webViewRef = useRef(null);
  const router = useRouter();

  const khaltiCheckoutHTML = `
    <html>
      <head>
        <script src="https://khalti.com/static/khalti-checkout.js"></script>
      </head>
      <body>
        <button id="payment-button" style="
  background-color: #5D2E8C;
  color: white;
  border: none;
  padding: 12px 24px;
  font-size: 16px;
  border-radius: 8px;
  cursor: pointer;
  margin:180px;
">Pay with Khalti</button>
        <script>
          var config = {
            "publicKey": "test_public_key_dc74c65a9e3a49c2b7cc1525a56f6b4a",
            "productIdentity": "pid123",
            "productName": "Test Product",
            "productUrl": "http://example.com/product",
            "amount": 1000,

            eventHandler: {
              onSuccess(payload) {
                window.ReactNativeWebView.postMessage(JSON.stringify(payload));
              },
              onError(error) {
                window.ReactNativeWebView.postMessage(JSON.stringify({ error }));
              },
              onClose() {
                console.log('widget is closing');
              }
            }
          };
          var checkout = new KhaltiCheckout(config);
          document.getElementById("payment-button").onclick = function () {
            checkout.show({amount: 1000});
          }
        </script>
      </body>
    </html>
  `;

  const handleMessage = async (event: any) => {
    const data = JSON.parse(event.nativeEvent.data);

    if (data.token && data.amount) {
      try {
        const response = await axios.post(
          "https://23e3606805ca.ngrok-free.app/api/Payment/add",
          {
            token: data.token,
            amount: data.amount,
            pid: "1",
          }
        );

        if (response.data.success) {
          Alert.alert("Success", "Payment verified and added!");
          router.push("/payments/success");
        } else {
          Alert.alert(
            "Failed",
            response.data.message || "Payment verification failed."
          );
        }
      } catch (error: any) {
        Alert.alert(
          "Error",
          error?.response?.data?.message || "Something went wrong."
        );
      }
    } else if (data.error) {
      Alert.alert("Khalti Error", JSON.stringify(data.error));
    }
  };

  return (
    <WebView
      originWhitelist={["*"]}
      ref={webViewRef}
      source={{ html: khaltiCheckoutHTML }}
      onMessage={handleMessage}
      startInLoadingState
      renderLoading={() => (
        <View style={{ flex: 1, justifyContent: "center" }}>
          <ActivityIndicator size="large" />
        </View>
      )}
    />
  );
}
