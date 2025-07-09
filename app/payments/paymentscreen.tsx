import React, { useState } from 'react';
import { View, Button, Alert, StyleSheet } from 'react-native';
import axios from 'axios';
import KhaltiPayment from './khaltipayment';
import axiosInstance from '@/services/axiosInstance';
import { API_ENDPOINTS } from '@/services/endPoints';
const { Payments } = API_ENDPOINTS;

const PaymentScreen: React.FC = () => {
  const [showKhalti, setShowKhalti] = useState(false);

  const verifyAndAddPayment = async (token: string, amount: number) => {
    try {
      // 🔹 Step 1: Verify with Khalti API
      const verifyRes = await axios.post(
        'https://khalti.com/api/v2/payment/verify/',
        { token, amount },
        {
          headers: {
            Authorization: "", // 🔁 Replace with your test secret key
            'Content-Type': 'application/json',
          },
        }
      );

      if (verifyRes.status === 200) {
        Alert.alert('✅ Verified', 'Khalti payment verified successfully.');

        // 🔹 Step 2: Call your backend API to store the payment
        const addPaymentRes = await axiosInstance.post(
          Payments , 
          {
            pid: '1',
            amount: amount / 100, // Convert paisa to NPR
          }
        );

        if (addPaymentRes.data?.success) {
          Alert.alert('🎉 Success', 'Payment recorded successfully.');
        } else {
          Alert.alert('⚠️ Recorded Failed', 'Verification passed, but saving failed.');
        }
      } else {
        Alert.alert('❌ Verification Failed', 'Could not verify payment.');
      }
    } catch (error: any) {
      console.log('Payment Error:', error.response?.data || error);
      Alert.alert('❌ Error', 'Something went wrong during payment.');
    }
  };

  return (
    <View style={styles.container}>
      {showKhalti ? (
        <KhaltiPayment
          amount={10} // Amount in NPR
          productName="Test Product"
          onSuccess={(payload) => {
            verifyAndAddPayment(payload.token, payload.amount);
            setShowKhalti(false);
          }}
          onCancel={(error) => {
            Alert.alert('Payment Cancelled', 'User cancelled or error occurred.');
            console.log('Khalti cancel/error:', error);
            setShowKhalti(false);
          }}
        />
      ) : (
        <View className='mt-96'>
        <Button title="Pay with Khalti" onPress={() => setShowKhalti(true)} />
            </View>
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    padding: 16,
  },
});

export default PaymentScreen;
