import { View, Text } from "react-native";
import React from "react";
import { Tabs } from "expo-router";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import Entypo from '@expo/vector-icons/Entypo';

const TabIcon = ({
  focused,
  icon,
  title,
}: {
  focused: boolean;
  icon: any;
  title: string;
}) => (
  <View className="flex-1  flex-col items-center">
    <Entypo
      name={icon}
      size={20}
      color={focused ? "#7A4DFF" : "#666876"}
    />
    <Text
      className={`${
        focused
          ? "text-primary-100 font-rubik-Medium"
          : "text-black-200 font-rubik"
      } text-xs w-full text-center mt-1`}
    >
      {title}
    </Text>
  </View>
  // <View style={{ flex: 1, alignItems: "center" }}>
  //   <Entypo name={icon} size={20} color={focused ? "#7A4DFF" : "#666876"} />
  //   <Text
  //     style={{
  //       fontSize: 12,
  //       textAlign: "center",
  //       marginTop: 4,
  //       color: focused ? "#7A4DFF" : "#666876",
  //       fontWeight: focused ? "600" : "400",
  //     }}
  //   >
  //     {title}
  //   </Text>
  // </View>
);

// const TabIcon = ({
//   focused,
//   icon,
//   title,
// }: {
//   focused: boolean;
//   icon: any;
//   title: string;
// }) => {
//     if (focused){
      
  
//   return (
   
//     <View className="flex flex-row w-full flex-1 bg-[#aa4aeb] min-w-[112px] min-h-14 m-4  justify-center items-center rounded-full overflow-hidden">
//       <FontAwesome name={icon} size={20} color="black" />
//       <Text className="text-secondary text-base font-semibold ml-2">
//         {title}
//       </Text>
//     </View>
//   );
//     }
//     return (
//       <View className="size-full justify-center items-center mt-4 rounded-full">
//         <FontAwesome name={icon} size={20} color="#A8B5DB" />
//       </View>
//     )
// };
const TabsLayout = () => {
    return(

  <Tabs
    screenOptions={{
      tabBarShowLabel: false,
      tabBarStyle: {
        backgroundColor: "white",
        position: "absolute",
        borderTopColor: "#0061FF1A",
        borderTopWidth: 1,
        minHeight: 70,
      },
    }}
  >
    <Tabs.Screen
        name="index"
        options={{
          title: "Home",
          headerShown: false,
          tabBarIcon: ({ focused }) => 
             <TabIcon focused={focused} icon="home" title="Home" />,
         
        }}
      />

    <Tabs.Screen
      name="showpost"
      options={{
        title: "Post",
        headerShown: false,
        tabBarIcon: ({ focused }) => (
          <TabIcon focused={focused} icon="progress-full" title="Posts" />
        ),
      }}
    />
    <Tabs.Screen
      name="explore"
      options={{
        title: "Booking",
        headerShown: false,
        tabBarIcon: ({ focused }) => (
          <TabIcon focused={focused} icon="bookmark" title="Booking" />
        ),
      }}
    />
    <Tabs.Screen
      name="profile"
      options={{
        title: "Profile",
        headerShown: false,
        tabBarIcon: ({ focused }) => (
          <TabIcon focused={focused} icon="user" title="Profile" />
        ),
      }}
    />
  </Tabs>
    )
};

export default TabsLayout;
