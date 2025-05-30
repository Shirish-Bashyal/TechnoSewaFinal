import { View, Text } from 'react-native'
import React from 'react'
import { useLocalSearchParams } from 'expo-router'

const TechDetails = () => {
    const {id}=useLocalSearchParams();
  return (
    <View>
      <Text> TechDetails</Text>
    </View>
  )
}

export default TechDetails