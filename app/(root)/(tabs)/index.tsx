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

export default function Index() {
  const router = useRouter();

  return (
    <SafeAreaView className="bg-gray-100 h-full">
      <FlatList
        data={[1, 2, 3, 4]}
        renderItem={({ item }) => <Card />}
        keyExtractor={(item) => item.toString()}
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
                <TouchableOpacity className="border-[1px] h-8 mt-0.5 rounded-full">
                  <Ionicons name="notifications" size={24} color="black" />
                </TouchableOpacity>
                <TouchableOpacity className="flex flex-row items-center bg-gray-200/85 px-3 py-1.5 rounded-full ">
                  <Ionicons name="location-outline" size={24} color="black" />
                  <Text
                    className="text-xs font-rubik-bold text-black-300 "
                    style={{ fontFamily: "rubik-bold" }}
                  >
                    Butwal
                  </Text>
                </TouchableOpacity>
              </View>{" "}
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
                  Adrian
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
                data={[1, 2, 3]}
                renderItem={({ item }) => <FeaturedCard />}
                keyExtractor={(item) => item.toString()}
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
                <Text
                  className="text-xs underline font-rubik-bold text-primary-100"
                  style={{ fontFamily: "outfit-medium" }}
                >
                  View more
                </Text>
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
              <TouchableOpacity onPress={() => router.push("/sign-in")}>
                <Text
                  className="text-xs underline font-rubik-bold text-primary-100"
                  style={{ fontFamily: "outfit-medium" }}
                >
                  Sign In
                </Text>
              </TouchableOpacity>
            </View>
          </View>
        }
      />
    </SafeAreaView>
  );
}
