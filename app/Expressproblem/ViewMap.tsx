import React, { useRef } from "react";
import { View, StyleSheet, Dimensions } from "react-native";
import { WebView } from "react-native-webview";
import { leafletViewMapHtml } from "./leafletMapHtml";

const { width } = Dimensions.get("window");

type Props = {
  latitude: number;
  longitude: number;
  onSelectLocation: (lat: number, lng: number) => void;
};

const LeafletViewMap = ({ latitude, longitude, onSelectLocation }: Props) => {
  const webviewRef = useRef<WebView>(null);

  const onMessage = (event: any) => {
    try {
      const data = JSON.parse(event.nativeEvent.data);
      if (data.latitude && data.longitude) {
        onSelectLocation(data.latitude, data.longitude);
      }
    } catch (e) {
      console.warn("Failed to parse message from WebView:", e);
    }
  };

  return (
    <View style={styles.container}>
      <WebView
        ref={webviewRef}
        originWhitelist={['*']}
        source={{ html: leafletViewMapHtml }}
        style={styles.webview}
        onLoad={() => {
          const msg = JSON.stringify({ latitude, longitude });
          webviewRef.current?.postMessage(msg);
        }}
        onMessage={onMessage}
        javaScriptEnabled={true}
        domStorageEnabled={true}
      />
    </View>
  );
};

export default LeafletViewMap;

const styles = StyleSheet.create({
  container: {
    margin: 10,
    height: 350,
    borderRadius: 10,
    overflow: "hidden",
  },
  webview: {
    width: width - 20,
    height: 300,
  },
});
