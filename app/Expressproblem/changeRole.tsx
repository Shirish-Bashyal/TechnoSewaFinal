import { View, Text } from 'react-native'
import React,{useState,useEffect} from 'react'
import { Button, Menu, Provider } from 'react-native-paper';
import { useForm } from 'react-hook-form';
import { useChangeRole } from '@/services/api/changeRole';

type FormValues = {
  role: string;
};
const changeRole = () => {
  const { register, setValue, watch } = useForm<FormValues>();
  const [menuVisible, setMenuVisible] = useState(false);

  const selectedRole = watch('role');
    const { mutate:changeRole,isPending } = useChangeRole();

  useEffect(() => {
    register('role');
  }, [register]);

 const handleSelect = (role: string) => {
    setValue('role', role);
    setMenuVisible(false);
    changeRole({ role });
  };
  return (
    <View style={{ paddingTop: 50, flexDirection: 'row', justifyContent: 'center' }}>
        <Menu
          visible={menuVisible}
          onDismiss={() => setMenuVisible(false)}
          anchor={
            <Button mode="contained" loading={isPending} onPress={() => setMenuVisible(true)}>
              {selectedRole || 'Select Role'}
            </Button>
          }
        >
          <Menu.Item onPress={() => handleSelect('Consumer')} title="Consumer" />
          <Menu.Item onPress={() => handleSelect('Technician')} title="Technician" />
        </Menu>
      </View>
  )
}

export default changeRole