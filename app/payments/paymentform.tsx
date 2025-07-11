// app/payment-form.tsx
import { useRouter } from "expo-router";
import { useState } from "react";
import { View, Text, TextInput, Button, StyleSheet, Alert } from "react-native";

export default function PaymentForm() {
  const router = useRouter();
  const [amount, setAmount] = useState("");

  const handlePay = () => {
    if (!amount || isNaN(Number(amount))) {
      Alert.alert("Invalid Input", "Please enter a valid amount.");
      return;
    }
    router.push(`/payments/testkhalti?amount=${amount}`);
  };

  return (
    <View style={styles.container}>
      <Text style={styles.label}>Enter Amount:</Text>
      <TextInput
        style={styles.input}
        keyboardType="numeric"
        value={amount}
        onChangeText={setAmount}
        placeholder="e.g. 100"
      />
      <Button title="Pay with Khalti" onPress={handlePay} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, justifyContent: "center", padding: 20 },
  label: { fontSize: 18, marginBottom: 10 },
  input: {
    borderWidth: 1,
    padding: 10,
    fontSize: 16,
    marginBottom: 20,
    borderRadius: 5,
  },
});
