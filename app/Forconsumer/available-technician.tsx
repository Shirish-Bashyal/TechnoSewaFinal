// app/available-technicians.tsx
import { Link, useLocalSearchParams } from "expo-router";
import { useViewAvailableTechnician } from "@/services/api/availabletechnician";
import {
  View,
  Text,
  ActivityIndicator,
  ScrollView,
  TouchableOpacity,
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import FontAwesome from "@expo/vector-icons/FontAwesome";

const AvailableTechniciansPage = () => {
  const { Date, TimeFrameEnum, Latitude, Longitude } = useLocalSearchParams();

  const {
    data: availableTechnician,
    isLoading,
    error,
  } = useViewAvailableTechnician({
    Date: String(Date),
    TimeFrameEnum: Number(TimeFrameEnum),
    Latitude: Number(Latitude),
    Longitude: Number(Longitude),
  });

  return (
    <SafeAreaView>
      <ScrollView contentContainerStyle={{ padding: 20 }}>
        <Text style={{ fontSize: 20, fontWeight: "bold", marginBottom: 10 }}>
          Available Technicians
        </Text>

        {isLoading && <ActivityIndicator size="large" />}

        {availableTechnician?.data?.length === 0 && (
          <Text>No technicians found for selected criteria.</Text>
        )}

        {availableTechnician?.data?.map((tech) => (
          <View
            className="flex flex-col px-2 py-2 mb-4 h-auto w-full bg-white shadow-md shadow-zinc-400 rounded-lg"
            key={tech.technicianId}
          >
            <View>
              <Text
                className="text-base font-outfit-bold text-black-300 "
                style={{ fontFamily: "outfit-Medium" }}
              >
                {tech.name}
              </Text>
            </View>
            <View>
              <Text
                className="text-xs text-black-300"
                style={{ fontFamily: "rubik-light" }}
              >
                Distance: {tech.distance} km
              </Text>
            </View>
            <View className="flex flex-row gap-2 my-1">
              <View className="flex flex-row items-center px-4  bg-blue-500/10  rounded-full">
                <FontAwesome name="star-half-empty" size={18} color="gold" />
                <Text className="text-xs font-rubik-bold  ml-0.5">
                  {tech.reviews?.averageRating ?? 0}
                </Text>
              </View>
              <View>
                {tech.reviews?.reviews && tech.reviews.reviews.length > 0 ? (
                  <View className="flex flex-row gap-2">
                    {tech.reviews.reviews.map((review, index) => (
                      <View
                        key={index}
                        className="bg-blue-500/10 rounded-lg px-2 py-1"
                      >
                        <Text
                          className="text-xs text-primary-300"
                          style={{
                            fontStyle: "italic",
                            fontFamily: "rubik-light",
                          }}
                        >
                          {review}
                        </Text>
                      </View>
                    ))}
                  </View>
                ) : (
                  <View>
                    <Text></Text>
                  </View>
                )}
              </View>
            </View>
            <Link
              href={`/Forconsumer/subcategorybooking/${tech.technicianId}`}
              asChild
            >
              <TouchableOpacity
                // onPress={handleShowAboutTechnician}
                className="bg-[#7A4DFF]/[1.6] shadow-md w-[40%] shadow-zinc-300 rounded-lg  flex justify-center items-center h-12 py-4 mt-1 mr-4 "
              >
                <View>
                  <Text
                    className="text-xs  text-white text-center"
                    style={{ fontFamily: "rubik-bold" }}
                  >
                    Book Now
                  </Text>
                </View>
              </TouchableOpacity>
            </Link>
          </View>
        ))}
      </ScrollView>
    </SafeAreaView>
  );
};

export default AvailableTechniciansPage;
