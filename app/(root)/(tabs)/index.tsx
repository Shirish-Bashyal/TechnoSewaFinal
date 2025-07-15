import { FlatList, Image, Text, TouchableOpacity, View } from "react-native";
import { Link } from "expo-router";
import { SafeAreaView } from "react-native-safe-area-context";
import images from "@/constants/images";
import Ionicons from "@expo/vector-icons/Ionicons";
import Search from "@/components/search";
import { Card, FeaturedCard } from "@/components/Cards";
import Entypo from "@expo/vector-icons/Entypo";
import Categories from "@/components/Categories";
import { useRouter } from "expo-router";
import { useViewProfile } from "@/services/api/profile";
import MaterialCommunityIcons from "@expo/vector-icons/MaterialCommunityIcons";
import { useShowNotificationData } from "@/services/api/notification";
import { useNotificationSetup } from "@/app/Notification/usenotification";
import { useEffect } from "react";
import { triggerLocalNotification } from "@/app/Notification/triggernotification";
import * as Notifications from "expo-notifications";
import AsyncStorage from "@react-native-async-storage/async-storage";
const SHOWN_NOTIFICATION_IDS = 'SHOWN_NOTIFICATION_IDS';

const cardFeaturedData = [
  {
    id: 1,
    image: images.pipe,
    title: "Pipe Repairs",
    price: "4500",
    rating: "4.4",
  },
  {
    id: 2,
    image: images.switches,
    title: "Switch Repairs Package",
    price: "5000",
    rating: "4.8",
  },
  {
    id: 3,
    image: images.furniture,
    title: "Furniture Package",
    price: "6000",
    rating: "4.6",
  },
  {
    id: 4,
    image: images.wire,
    title: "Full Package",
    price: "3000",
    rating: "3.6",
  },
  {
    id: 5,
    image: images.tab,
    title: "Full Package",
    price: "2000",
    rating: "2.8",
  },
];
const cardData = [
  {
    id: 1,
    image: images.wire,
    title: "Electric wire repair",
    price: "500",
    rating: "3.6",
    reviews: "9",
    team: "By Team Nepal",
    description: "Electrical Appliance Installation",
  },
  {
    id: 2,
    image: images.tab,
    title: "Tab repairs",
    price: "200",
    rating: "4.8",
    reviews: "12",
    team: "By Repair Nepal",
    description: "Plumber Services",
  },
  {
    id: 3,
    image: images.pipe,
    title: "Pipe Repairs",
    price: "650",
    rating: "4.4",
    reviews: "8",
    team: "By Cool Air",
    description: "Pipe Repairs and Maintenance",
  },
  {
    id: 4,
    image: images.switches,
    title: "Switch Repairs Package",
    price: "400",
    rating: "3.8",
    reviews: "5",
    team: "By Team Nepal",
    description: "Switch Installation",
  },
  {
    id: 5,
    image: images.furniture,
    title: "Furniture Package",
    price: "600",
    rating: "2.6",
    reviews: "11",
    team: "By Furniture Nepal",
    description: "Furniture Installation",
  },
];

Notifications.setNotificationHandler({
  handleNotification: async () => ({
    shouldShowAlert: true,
    shouldPlaySound: true,
    shouldSetBadge: false,
    shouldShowBanner: true,
    shouldShowList: true,
  }),
});
export default function Index() {
  const router = useRouter();
  const { data: profileData, isError, isLoading } = useViewProfile();
  const { data: notiData } = useShowNotificationData();
  const { data, error } = useShowNotificationData();
  useNotificationSetup();

  // useEffect(() => { //Test data
  //   // Slight delay to ensure permissions and channel setup complete
  //   const timer = setTimeout(() => {
  //     triggerLocalNotification(
  //       "👋 Welcome Back!",
  //       "Thanks for opening the app!"
  //     );
  //   }, 3000);

  //   return () => clearTimeout(timer);
  // }, []);
  useEffect(() => {
    if (data && data.success && data.data && data.data.length > 0) {
      // Loop through notifications and trigger local notifications
      data.data.forEach((noti) => {
        triggerLocalNotification(`🎉 ${noti.title}`, noti.message);
      });
    }
  }, [data]);
//   useEffect(() => {
//   const checkAndTriggerNewNotifications = async () => {
//     if (!data || !data.success || !data.data || data.data.length === 0) return;

//     try {
//       const shownIdsString = await AsyncStorage.getItem(SHOWN_NOTIFICATION_IDS);
//       const shownIds: string[] = shownIdsString ? JSON.parse(shownIdsString) : [];

//       const newNotifications = data.data.filter(
//         (noti) => !shownIds.includes(noti.receivedDate) // assuming receivedDate is unique
//       );

//       if (newNotifications.length > 0) {
//         for (const noti of newNotifications) {
//           await triggerLocalNotification(`🎉 ${noti.title}`, noti.message);
//         }

//         // Save new shown IDs
//         const updatedIds = [...shownIds, ...newNotifications.map(n => n.receivedDate)];
//         await AsyncStorage.setItem(SHOWN_NOTIFICATION_IDS, JSON.stringify(updatedIds));
//       }
//     } catch (err) {
//       console.error('Failed to handle notifications:', err);
//     }
//   };

//   checkAndTriggerNewNotifications();
// }, [data]);

  const handleShowNotification = () => {
    router.push("/Bookings/notification");
  };

  const handleShowChatbot = () => {
    router.push("/chatbot/chats");
  };

  return (
    <SafeAreaView className="bg-gray-100 h-full">
      <FlatList
        data={cardData}
        renderItem={({ item }) => (
          <Card
            image={item.image}
            title={item.title}
            price={item.price}
            rating={item.rating}
            description={item.description}
            team={item.team}
            reviews={item.reviews}
          />
        )}
        keyExtractor={(item) => item.id.toString()}
        numColumns={2}
        contentContainerClassName="pb-32"
        columnWrapperClassName="flex gap-5 px-5"
        showsVerticalScrollIndicator={false}
        ListHeaderComponent={
          <View className="px-5">
            <View className="flex flex-row items-center justify-between mt-5">
              <View className="flex flex-row items-center">
                <Image
                  source={images.logo}
                  className="!size-14  rounded-xl !border-black-100 border-[1px]"
                />
              </View>
              {/* <View className="flex flex-col items-start ml-2 justify-center">
                  <Text className="text-base  text-black-100" style={{fontFamily:'rubik'}}>
                    Welcome to Our Services
                  </Text>
                  
                </View> */}
              <View className="flex flex-row gap-4">
                <TouchableOpacity
                  className="border-[1px] h-8 w-10 px-1 mt-0.5 rounded-full"
                  onPress={handleShowNotification}
                >
                  <Ionicons name="notifications" size={24} color="black" />
                  {notiData?.data?.length && notiData.data.length > 0 && (
                    <View className="absolute -top-1 -right-1 bg-green-500 rounded-full w-5 h-5 items-center justify-center">
                      <Text className="text-white text-[10px] font-bold">
                        {notiData?.data?.length ?? 0}
                      </Text>
                    </View>
                  )}
                </TouchableOpacity>
                <TouchableOpacity className="flex flex-row items-center bg-gray-200/85 px-3 py-1.5 rounded-full ">
                  <Ionicons name="location-outline" size={24} color="black" />
                  <Text
                    className="text-xs font-rubik-bold text-black-300 "
                    style={{ fontFamily: "rubik-bold" }}
                  >
                    {profileData?.data?.city || "Butwal"}
                  </Text>
                </TouchableOpacity>
              </View>
            </View>

            <View className="flex flex-row items-center mt-5">
              {/* <Image
                  source={images.avatar}
                  className="!size-12  rounded-full"
                /> */}
              <View className="flex flex-row gap-1 items-start  justify-center">
                <Text
                  className="text-xl  text-black-300"
                  style={{ fontFamily: "rubik-medium" }}
                >
                  Hello,
                </Text>
                <Text
                  className="text-xl  text-primary-100"
                  style={{ fontFamily: "rubik-bold" }}
                >
                  {profileData?.data?.name || "User"} !!
                </Text>
              </View>
            </View>
            <View className=" mt-1 mb-1">
              <Text
                className="text-base text-black-200"
                style={{ fontFamily: "rubik-bold" }}
              >
                Welcome to Techno Sewa!!
              </Text>
            </View>

            <Search />
            <View className="my-5">
              <View className="flex flex-row items-center justify-between">
                <Text
                  className="text-xl font-rubik-bold text-black-300"
                  style={{ fontFamily: "rubik-bold" }}
                >
                  Featured
                </Text>
                <TouchableOpacity>
                  <Text
                    className="text-xs underline font-rubik-bold text-primary-100"
                    style={{ fontFamily: "outfit-medium" }}
                  >
                    View more
                  </Text>
                </TouchableOpacity>
              </View>

              {/* //{"New Services "} */}
              <FlatList
                data={cardFeaturedData}
                renderItem={({ item }) => (
                  <FeaturedCard
                    image={item.image}
                    title={item.title}
                    price={item.price}
                    rating={item.rating}
                  />
                )}
                keyExtractor={(item) => item.id.toString()}
                horizontal
                bounces={false}
                showsHorizontalScrollIndicator={false}
                contentContainerClassName="flex gap-2 mt-5"
              />
            </View>

            <View className="flex flex-row items-center justify-between mt-4">
              <Text
                className="text-xl text-black-300"
                style={{ fontFamily: "rubik-bold" }}
              >
                Categories
              </Text>
              <TouchableOpacity>
                <View>
                  <Text
                    className="text-xs underline font-rubik-bold text-primary-100"
                    style={{ fontFamily: "outfit-medium" }}
                  >
                    View more
                  </Text>
                </View>
              </TouchableOpacity>
            </View>
            <Categories />

            <View className="flex flex-row items-center justify-between mt-4">
              <Text
                className="text-xl text-black-300"
                style={{ fontFamily: "rubik-bold" }}
              >
                New Services
              </Text>
              <TouchableOpacity>
                <View>
                  <Text
                    className="text-xs underline font-rubik-bold text-primary-100"
                    style={{ fontFamily: "outfit-medium" }}
                  >
                    View More
                  </Text>
                </View>
              </TouchableOpacity>
            </View>
          </View>
        }
      />

      {/* ChatBot */}
      <TouchableOpacity
        onPress={handleShowChatbot}
        style={{
          position: "absolute",
          top: 280,
          right: 20,
          backgroundColor: "#007AFF",
          borderRadius: 50,
          padding: 16,
          elevation: 10,
          shadowColor: "#000",
          shadowOffset: { width: 0, height: 2 },
          shadowOpacity: 0.3,
          shadowRadius: 3,
          zIndex: 100,
        }}
      >
        {/* <Ionicons name="chatbubbles-outline" size={24} color="white" /> */}
        <MaterialCommunityIcons name="robot-outline" size={24} color="white" />
      </TouchableOpacity>
    </SafeAreaView>
  );
}
