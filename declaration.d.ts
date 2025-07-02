// declarations.d.ts
declare module "@teovilla/react-native-web-maps" {
  import { ComponentType } from "react";
  import { ViewStyle } from "react-native";

  interface MapProps {
    style: ViewStyle;
    initialRegion?: {
      latitude: number;
      longitude: number;
      latitudeDelta: number;
      longitudeDelta: number;
    };
    onPress?: (event: any) => void;
    children?: React.ReactNode;
  }

  const MapView: ComponentType<MapProps> & {
    Marker: ComponentType<{ coordinate: { latitude: number; longitude: number } }>;
  };

  export default MapView;
}
