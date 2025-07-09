// app/success.tsx
import { View, Text, Button, StyleSheet } from "react-native";
import { useRouter } from "expo-router";

export default function Success() {
  const router = useRouter();
  return (
    <View style={styles.container}>
      <Text style={styles.title}>✅ Payment Successful</Text>
      <Button title="Go to Form" onPress={() => router.replace("/payments/paymentform")} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, justifyContent: "center", alignItems: "center" },
  title: { fontSize: 22, fontWeight: "bold", color: "green", marginBottom: 20 },
});
