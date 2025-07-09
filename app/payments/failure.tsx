// app/failure.tsx
import { View, Text, Button, StyleSheet } from "react-native";
import { useRouter } from "expo-router";

export default function Failure() {
  const router = useRouter();
  return (
    <View style={styles.container}>
      <Text style={styles.title}>❌ Payment Failed</Text>
      <Button title="Try Again" onPress={() => router.replace("/payments/paymentform")} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, justifyContent: "center", alignItems: "center" },
  title: { fontSize: 22, fontWeight: "bold", color: "red", marginBottom: 20 },
});
