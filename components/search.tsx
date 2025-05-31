import { View, Text, TextInput, TouchableOpacity } from "react-native";
import React, { useState } from "react";
import { useLocalSearchParams, usePathname,router } from "expo-router";
import EvilIcons from "@expo/vector-icons/EvilIcons";
import {useDebouncedCallback} from "use-debounce";

const Search = () => {
  const path = usePathname();
  const params = useLocalSearchParams<{ query?: string }>();
  const [search, setSearch] = useState(params.query);

const debouncedSearch = useDebouncedCallback(
  (text: string) => router.setParams({ query: text }),
  500
);

  const handleSearch = (text: string) => {
    setSearch(text);
    debouncedSearch(text);
  };
  return (
    <View className="flex flex-row items-center justify-between w-[98%] mx-2 px-4 rounded-lg bg-accent-100 border-primary-100 mt-5 py-2">
      <View className="flex-1 flex flex-row items-center justify-start z-50">
        <EvilIcons name="search" size={24} color="black" />
        <TextInput
          value={search}
          onChangeText={handleSearch}
          placeholder="Search for anything"
          className="w-full text-sm font-rubik text-black-300"
        />
      </View>
      <TouchableOpacity></TouchableOpacity>
    </View>
  );
};

export default Search;
