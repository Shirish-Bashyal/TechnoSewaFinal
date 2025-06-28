import {
  View,
  Text,
  TouchableOpacity,
  ScrollView,
  TextInput,
  StyleSheet,
  Button,
  Image,
} from "react-native";
import React, { useEffect, useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { useRouter } from "expo-router";
import AntDesign from "@expo/vector-icons/AntDesign";
import { MultipleSelectList } from "react-native-dropdown-select-list";
import { SelectList } from "react-native-dropdown-select-list";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import { useForm } from "react-hook-form";
import * as ImagePicker from "expo-image-picker";
import { usePostProblem } from "@/services/api/postProblem";

type ImageFile = {
  uri: string;
  type: string;
  name: string;
   fileName?: string; 
};

type FormValues = {
  Title: string;
  Category: string;
  Description: string;
  ImagesFiles: ImageFile[];
  Lattitude: number;
  Longitude: number;
};

const ExpressProblem = () => {
  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm<FormValues>();
  const router = useRouter();
  const [selected, setSelected] = React.useState<string>("");
  const [text, setText] = React.useState("");
  // const [image, setImage] = useState<string | null>(null); //for single image
  const [image, setImage] = useState<string[]>([]); // multiple images
  const [selectedImageFiles, setSelectedImageFiles] = useState<ImageFile[]>([]);
   const [imagePreviewUris, setImagePreviewUris] = useState<string[]>([]);
  const data = [
    { key: "1", value: "plumbing" },
    { key: "2", value: "Electrician" },
    { key: "3", value: "House Keeping" },
    { key: "4", value: "Automobiles" },
    { key: "5", value: "Tech Experts" },
    { key: "6", value: "Carpenter" },
  ];
  const { mutate: postProblemForm, isPending } = usePostProblem();

  // const pickImage = async () => {
  //   // No permissions request is necessary for launching the image library
  //   let result = await ImagePicker.launchImageLibraryAsync({
  //     mediaTypes: ['images', 'videos'],
  //     allowsEditing: true,
  //     aspect: [4, 3],
  //     quality: 1,
  //   });

  //   console.log(result);

  //   if (!result.canceled) {
  //     setImage(result.assets[0].uri);
  //   }
  // };
  const pickImage = async () => {
    const result = await ImagePicker.launchImageLibraryAsync({
      allowsMultipleSelection: true,
      mediaTypes: ["images", "videos"],
      quality: 1,
      base64: false,
    });

    // if (!result.canceled) {
    //   const newUris = result.assets.map((asset) => asset.uri);
    //   // const updatedImages = [...ImagesFiles, ...newUris];
    //   setImage(newUris);
    //   setValue("ImagesFiles", newUris);

    // }
     if (!result.canceled) {
      const newImageFiles: ImageFile[] = result.assets.map((asset) => {
        // Essential: Extract the correct filename and infer MIME type
        const filename = asset.fileName || `image_${Date.now()}.${asset.type === 'image' ? 'jpg' : 'mp4'}`;
        const uri = asset.uri;

        // Ensure a valid MIME type. ImagePicker asset.type is 'image' or 'video'.
        // asset.mimeType is more reliable if available.
        let mimeType = asset.mimeType || (asset.type === 'image' ? 'image/jpeg' : 'video/mp4'); // Default fallback

        // A more robust way to get MIME type from extension
        const ext = filename.split('.').pop()?.toLowerCase();
        if (ext) {
            switch (ext) {
                case 'jpg':
                case 'jpeg': mimeType = 'image/jpeg'; break;
                case 'png': mimeType = 'image/png'; break;
                case 'gif': mimeType = 'image/gif'; break;
                case 'bmp': mimeType = 'image/bmp'; break;
                case 'webp': mimeType = 'image/webp'; break;
                case 'mp4': mimeType = 'video/mp4'; break;
                case 'mov': mimeType = 'video/quicktime'; break;
                // Add more as needed
            }
        }

        return {
          uri: uri,
          name: filename,
          type: mimeType,
        };
      });

      // Update states
      setImagePreviewUris((prevUris) => [...prevUris, ...newImageFiles.map((file) => file.uri)]);
      setSelectedImageFiles((prevFiles) => {
        const updatedFiles = [...prevFiles, ...newImageFiles];
        setValue("ImagesFiles", updatedFiles, { shouldValidate: true });
        return updatedFiles;
      });
    }
  };

  //  if (!result.canceled) {
  //   setImage(result.assets);
  //   setValue("ImagesFiles", result.assets, { shouldValidate: true });
  // }
  //     if (!result.canceled) {
  //   const imageFiles: ImageFile[] = result.assets.map((asset, index) => {
  //     const uri = asset.uri;
  //     const fileName = asset.fileName || `image_${index}.jpg`;
  //     const ext = fileName.split('.').pop()?.toLowerCase();
  //     let mimeType = 'image/jpeg';

  //     if (ext === 'png') mimeType = 'image/png';
  //     else if (ext === 'jpg' || ext === 'jpeg') mimeType = 'image/jpeg';

  //     return {
  //       uri,
  //       name: fileName,
  //       type: mimeType,
  //        fileName,
  //     };
  //   });

  //   console.log("Prepared image files:", imageFiles); // ✅ Debugging line

  //   setImage(imageFiles.map(img => img.uri)); // For preview only
  //   setValue("ImagesFiles", imageFiles, { shouldValidate: true });
  // }
  //     if (!result.canceled) {
  //   const newFiles = result.assets.map(asset => ({
  //     uri: asset.uri,
  //     name: asset.fileName ?? `image_${Math.random().toString(36).slice(2)}.jpg`,
  //     type: asset.type === "image" ? `image/${(asset.fileName?.split('.').pop() ?? "jpeg").toLowerCase()}` : 'application/octet-stream',
  //   }));

  //   setImage(newFiles.map(f => f.uri)); // for UI preview
  //   setValue("ImagesFiles", newFiles, { shouldValidate: true });
  // }
  //   const createFormData = (data: FormValues) => {
  //   const formData = new FormData();

  //   formData.append("Title", data.Title);
  //   formData.append("Category", data.Category);
  //   formData.append("Description", data.Description);
  //   formData.append("Lattitude", String(data.Lattitude));
  //   formData.append("Longitude", String(data.Longitude));

  //   data.ImagesFiles.forEach((image, index) => {
  //     const uri =  image.uri.startsWith("file://")
  //       ? image.uri.substring(7)
  //       : image.uri;
  //     const name = image.fileName || `image_${index}.jpg`;
  //     const type = image.type ? `image/${image.type}` : "image/jpeg";

  //     formData.append("ImagesFiles", { uri, name, type } as any);
  //   });

  //   return formData;
  // };
 const createFormData = (data: FormValues) => {
    const formData = new FormData();

    formData.append("Title", data.Title);
    formData.append("Category", data.Category);
    formData.append("Description", data.Description);
    formData.append("Lattitude", String(data.Lattitude));
    formData.append("Longitude", String(data.Longitude));

   const fileNames = data.ImagesFiles.map((image, index) => {
    return (
      image.fileName ||                  // preferred
      image.name ||                      // fallback
      decodeURIComponent(image.uri.split("/").pop() || "") || // uri fallback
      `image_${index}.jpg`               // final fallback
    );
  });

  // Append as JSON string
  formData.append("ImagesFiles", JSON.stringify(fileNames));


  return formData;
};

  const submitProblemData = (data: FormValues) => {
    const formData = createFormData(data);
    postProblemForm(formData);
  };

  //     const submitProblemData = async (data: FormValues) => {
  //   const formData = new FormData();

  //   formData.append("Title", data.Title);
  //   formData.append("Category", data.Category);
  //   formData.append("Description", data.Description);
  //   formData.append("Lattitude", String(data.Lattitude)); // send number as string
  //   formData.append("Longitude", String(data.Longitude)); // send number as string

  // // data.ImagesFiles.forEach((file, index) => {
  // //   const fileName = file.name || `image_${index}.jpg`;
  // //   const extension = fileName.split('.').pop()?.toLowerCase();

  // //   let mimeType = 'image/jpeg'; // default mime type

  // //   if (extension === 'png') {
  // //     mimeType = 'image/png';
  // //   } else if (extension === 'jpg' || extension === 'jpeg') {
  // //     mimeType = 'image/jpeg';
  // //   }

  // //   formData.append("ImagesFiles", {
  // //     uri: file.uri,
  // //     name: fileName,
  // //     type: mimeType,
  // //   } as any);
  // // });
  //  data.ImagesFiles.forEach((file: ImageFile, index) => {
  //     // Ensure a fallback name if none exists
  //     const fileName = file.name || file.fileName || `image_${index}.jpg`;

  //     formData.append("ImagesFiles", {
  //       uri: file.uri,
  //       name: fileName,
  //       type: file.type || "image/jpeg",
  //     } as any); // as any bypasses TypeScript limitations in React Native
  //   });

  //   postProblemForm(formData as any);
  //   };
  useEffect(() => {
    register("Title", { required: "Title is required" });
    register("Category", { required: "Category is required" });
    register("Description", { required: "Description is required" });
    register("ImagesFiles", {
      validate: (value) =>
        (value && value.length > 0) || "At least one image is required",
    });
    register("Lattitude", {
      required: "Lattitude is required",
      valueAsNumber: true,
    });
    register("Longitude", {
      required: "Longitude is required",
      valueAsNumber: true,
    });
  }, [register]);

  return (
    <SafeAreaView className="bg-gray-100 h-full">
      <View className="absolute bg-primary-100/70 h-28 flex justify-center top-0 left-0 right-0 z-10">
        <TouchableOpacity
          onPress={router.back}
          className="flex flex-row  gap-4 mx-4 py-10"
        >
          <AntDesign name="back" size={34} color="white" />
          <Text
            className="text-white   text-[20px]"
            style={{ fontFamily: "rubik-bold", letterSpacing: 1.5 }}
          >
            Express Your Problem
          </Text>
        </TouchableOpacity>
      </View>
      <ScrollView className="mt-28 px-2">
        <View>
          <View className="my-2">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Select Category
            </Text>
          </View>
          <SelectList
            setSelected={(key: any) => {
              const selectedCategory = data.find(
                (item) => item.key === key
              )?.value;
              if (selectedCategory) {
                setSelected(selectedCategory);
                setValue("Category", selectedCategory, {
                  shouldValidate: true,
                });
              }
            }}
            data={data}
            boxStyles={{ borderRadius: 8, borderColor: "#ccc" }}
            defaultOption={{ key: "1", value: "plumbing" }}
          />
        </View>
        <View className="mt-4">
          <View className="mb-2 flex flex-row gap-">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Enter Title of the Problem
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
          </View>
          <TextInput
            placeholder="Water pipe leakage"
            style={inputStyle}
            placeholderTextColor="#999"
            onChangeText={(text) => {
              setValue("Title", text, { shouldValidate: true });
            }}
            {...register("Title", {
              required: "Title is required",
            })}
          />
        </View>
        <View className="mt-4">
          <View className="mb-2 flex flex-row gap-">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Describe the Problem
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
          </View>

          <TextInput
            value={text}
            onChangeText={(val) => {
              setText(val);
              setValue("Description", val, { shouldValidate: true });
            }}
            placeholder="Write your message..."
            multiline
            numberOfLines={4}
            style={styles.textArea}
            placeholderTextColor="#999"
          />
        </View>
        <View className="mt-4">
          <View className="mb-2 flex flex-row gap-">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Upload Image
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
          </View>
        </View>
        <View style={styles.container}>
          {/* <Button title="Pick an image from camera roll" onPress={pickImage} /> */}
          {/* {errors.ImagesFiles && (
            <Text style={{ color: "red" }}>{errors.ImagesFiles.message}</Text>
          )} */}
          {/* for single image */}
          {/* {image && <Image source={{ uri: image }} style={styles.image} />} */}
          {/* <View style={{ flexDirection: "row", flexWrap: "wrap" }}>
            {image.map((uri, index) => (
              <Image
                key={index}
                source={{ uri }}
                style={{ width: 100, height: 100, margin: 5, borderRadius: 8 }}
              />
            ))}
          </View> */}
           <Button title="Pick an image from camera roll" onPress={pickImage} />
          {errors.ImagesFiles && (
            <Text style={{ color: "red" }}>{errors.ImagesFiles.message}</Text>
          )}
          <View style={{ flexDirection: "row", flexWrap: "wrap" }}>
            {imagePreviewUris.map((uri, index) => (
              <Image
                key={index}
                source={{ uri }}
                style={{ width: 100, height: 100, margin: 5, borderRadius: 8 }}
              />
            ))}
          </View>
        </View>
        <View className="mt-4">
          <View className="mb-2 flex flex-row gap-">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Lattitude
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
          </View>
          <TextInput
            placeholder="Lattitude"
            style={inputStyle}
            keyboardType="phone-pad"
            maxLength={4}
            placeholderTextColor="#999"
            onChangeText={(text) =>
              setValue("Lattitude", parseFloat(text) || 0, {
                shouldValidate: true,
              })
            }
          />
        </View>
        <View className="mt-4">
          <View className="mb-2 flex flex-row gap-0.5">
            <Text
              className="text-base text-black-300 "
              style={{ fontFamily: "outfit-light" }}
            >
              Longitude
            </Text>
            <Text className="text-red-600 text-base ">*</Text>
          </View>
          <TextInput
            placeholder="Longitude"
            style={inputStyle}
            keyboardType="phone-pad"
            maxLength={4}
            placeholderTextColor="#999"
            onChangeText={(text) =>
              setValue("Longitude", parseFloat(text) || 0, {
                shouldValidate: true,
              })
            }
          />
        </View>
        <Button
          title="Submit"
          onPress={handleSubmit(submitProblemData)}
          color="#7A4DFF"
        />
      </ScrollView>
    </SafeAreaView>
  );
};

export default ExpressProblem;

const inputStyle = {
  borderWidth: 1,
  borderColor: "#ccc",
  padding: 10,
  borderRadius: 8,
};

const styles = StyleSheet.create({
  textArea: {
    height: 120,
    textAlignVertical: "top",
    borderColor: "#ccc",
    borderWidth: 1,
    borderRadius: 8,
    padding: 12,
  },
  container: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
  },
  image: {
    width: 200,
    height: 200,
  },
});
