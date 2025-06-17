import { View} from 'react-native'
import React from 'react'
import { TextInput } from 'react-native-paper';
import { Card, Text } from 'react-native-paper';

import { StyleSheet } from 'react-native';

const Message = () => {
 const [visible, setVisible] = React.useState(false);

  const hideDialog = () => setVisible(false);
  return (
    <Card>
    <Card.Content>
      <Text variant="titleLarge">Card title</Text>
      <Text variant="bodyMedium">Card content</Text>
    </Card.Content>
  </Card>

  )
}

export default Message

const styles = StyleSheet.create({
  title: {
    textAlign: 'center',
  },
})