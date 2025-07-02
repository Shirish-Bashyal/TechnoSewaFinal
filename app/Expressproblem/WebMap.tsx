import React, { useState, useRef } from "react";
import { View, StyleSheet, Dimensions, Text } from "react-native";
import { WebView } from "react-native-webview";
import { leafletMapHtml } from "./leafletMapHtml";

const { width } = Dimensions.get("window");

type Props = {
  latitude: number;
  longitude: number;
  onSelectLocation: (lat: number, lng: number) => void;
};

const LeafletWebViewMap = ({ latitude, longitude, onSelectLocation }: Props) => {
  const [selectedCoord, setSelectedCoord] = useState({ latitude, longitude });
  const webviewRef = useRef<WebView>(null);

  // Handle message from WebView (the leaflet map)
  const onMessage = (event: any) => {
    try {
      const data = JSON.parse(event.nativeEvent.data);
      if (data.latitude && data.longitude) {
        setSelectedCoord({ latitude: data.latitude, longitude: data.longitude });
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
        source={{ html: leafletMapHtml }}
        style={styles.webview}
        onMessage={onMessage}
        javaScriptEnabled={true}
        domStorageEnabled={true}
      />
      <View style={styles.info}>
        <Text>Selected Location:</Text>
        <Text>
          Latitude: {selectedCoord.latitude.toFixed(5)}, Longitude: {selectedCoord.longitude.toFixed(5)}
        </Text>
      </View>
    </View>
  );
};

export default LeafletWebViewMap;

const styles = StyleSheet.create({
  container: {
    margin: 10,
    height: 550,
    borderRadius: 10,
    overflow: "hidden",
  },
  webview: {
    width: width - 20,
    height: 300,
  },
  info: {
    marginTop: 10,
    alignItems: "center",
  },
});
// import React, { useState } from "react";
// import { MapContainer, TileLayer, Marker, useMapEvent } from "react-leaflet";
// import "leaflet/dist/leaflet.css";
// import L from "leaflet";

// // Fix default marker icon path issue in Leaflet
// delete (L.Icon.Default as any).prototype._getIconUrl;
// L.Icon.Default.mergeOptions({
//   iconRetinaUrl:
//     "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon-2x.png",
//   iconUrl:
//     "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon.png",
//   shadowUrl:
//     "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png",
// });

// type Props = {
//   latitude: number;
//   longitude: number;
//   onSelectLocation: (lat: number, lng: number) => void;
// };

// const LocationPicker = ({
//   onSelect,
// }: {
//   onSelect: (lat: number, lng: number) => void;
// }) => {
//   useMapEvent("click", (e) => {
//     onSelect(e.latlng.lat, e.latlng.lng);
//   });
//   return null;
// };

// const WebMap = ({ latitude, longitude, onSelectLocation }: Props) => {
//   const [position, setPosition] = useState<[number, number]>([
//     latitude,
//     longitude,
//   ]);

//   const handleMapClick = (lat: number, lng: number) => {
//     setPosition([lat, lng]);
//     onSelectLocation(lat, lng);
//   };

//   return (
//     <div style={{ height: 300}}>
//       <MapContainer
//         center={position}
//         zoom={13}
//         scrollWheelZoom={true}
//         style={{ height: "100%", width: "100%" }}
//       >
//         <TileLayer
//           url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
//           attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
//         />
//         <Marker position={position} />
//         <LocationPicker onSelect={handleMapClick} />
//       </MapContainer>
//       <p style={{ textAlign: "center", marginTop: 10 }}>
//         Selected: Lat {position[0].toFixed(5)}, Lng {position[1].toFixed(5)}
//       </p>
//     </div>
//   );
// };

// export default WebMap;
