import React from 'react';
import { WebView } from 'react-native-webview';

interface KhaltiPaymentProps {
  amount: number; // Amount in NPR
  productName: string;
  onSuccess: (payload: { token: string; amount: number }) => void;
  onCancel: (error: any) => void;
}

const KhaltiPayment: React.FC<KhaltiPaymentProps> = ({
  amount,
  productName,
  onSuccess,
  onCancel,
}) => {
  const htmlContent = `
    <html>
      <head>
        <script src="https://khalti.com/static/khalti-checkout.js"></script>
      </head>
      <body>
        <button id="pay-button" style="
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
            publicKey: "",// 🔁 REPLACE with your test public key
            productIdentity: "1",
            productName: "${productName}",
            productUrl: "http://example.com",
            eventHandler: {
              onSuccess: function(payload) {
                window.ReactNativeWebView.postMessage(JSON.stringify({ type: "success", payload }));
              },
              onError: function(error) {
                window.ReactNativeWebView.postMessage(JSON.stringify({ type: "error", error }));
              },
              onClose: function() {
                window.ReactNativeWebView.postMessage(JSON.stringify({ type: "close" }));
              }
            }
          };
          var checkout = new KhaltiCheckout(config);
          document.getElementById("pay-button").onclick = function () {
            checkout.show({ amount: ${amount * 100} }); // amount in paisa
          };
        </script>
      </body>
    </html>
  `;

  return (
    <WebView
      originWhitelist={['*']}
      source={{ html: htmlContent }}
      onMessage={(event) => {
        const data = JSON.parse(event.nativeEvent.data);
        if (data.type === 'success') onSuccess(data.payload);
        else onCancel(data.error || 'cancelled');
      }}
    />
  );
};

export default KhaltiPayment;


