import React, { useRef, useState } from "react";
import {
  View,
  TextInput,
  TouchableOpacity,
  KeyboardAvoidingView,
  Platform,
  ScrollView,
  Text,
  StyleSheet,
} from "react-native";
import { useForm, Controller } from "react-hook-form";
import { Card } from "react-native-paper";
import { useMutation } from "@tanstack/react-query";
import { SafeAreaView } from "react-native-safe-area-context";
import { postQuestion } from "@/services/api/chat";
// adjust path

type Message = {
  id: number;
  type: "question" | "answer";
  text: string;
};

let messageId = 0;

const Chats = () => {
  const scrollRef = useRef<ScrollView>(null);
  const [messages, setMessages] = useState<Message[]>([]);
  const { control, handleSubmit, reset } = useForm<{ question: string }>({
    defaultValues: { question: "" },
  });

  const mutation = useMutation({
    mutationFn: postQuestion,
    onSuccess: (data) => {
      if (typeof data === "string") {
        addMessage("answer", data); //to show response data
      }
    },
  });

  const addMessage = (type: "question" | "answer", text: string) => {
    const newMessage: Message = {
      id: messageId++,
      type,
      text,
    };
    setMessages((prev) => [...prev, newMessage]);

    // Auto scroll to bottom after new message
    setTimeout(() => {
      scrollRef.current?.scrollToEnd({ animated: true });
    }, 100);
  };

  const onSubmit = (formData: { question: string }) => {
    const userQuestion = formData.question.trim();
    if (!userQuestion) return;

    addMessage("question", userQuestion);
    mutation.mutate({ question: userQuestion });
    reset(); // Clear input
  };
  return (
    <SafeAreaView style={{ flex: 1 }}>
      <View className="flex justify-center items-center mt-14">
        <Text className="text-base font-outfit-bold text-black mt-1"
                      style={{ fontFamily: "rubik-bold" }}>How Can I help you?</Text>
      </View>
      <KeyboardAvoidingView
        behavior="height"
        style={{ flex: 1 }}
        keyboardVerticalOffset={0}
      >
        <ScrollView
          ref={scrollRef}
          contentContainerStyle={styles.chatContainer}
          showsVerticalScrollIndicator={false}
        >
          {messages.map((msg) => (
            <View
              key={msg.id}
              style={[
                styles.messageWrapper,
                msg.type === "question" ? styles.right : styles.left,
              ]}
            >
              <Card
                style={[
                  styles.card,
                  msg.type === "question" ? styles.userCard : styles.botCard,
                ]}
              >
                <Text style={styles.messageText}>{msg.text}</Text>
              </Card>
            </View>
          ))}
        </ScrollView>

        <View style={styles.inputBar}>
          <Controller
            control={control}
            name="question"
            render={({ field: { onChange, value } }) => (
              <TextInput
                style={styles.input}
                placeholder="Ask something..."
                value={value}
                onChangeText={onChange}
                multiline
              />
            )}
          />
          <TouchableOpacity
            style={styles.sendButton}
            onPress={handleSubmit(onSubmit)}
          >
            <Text style={{ color: "#fff" }}>Send</Text>
          </TouchableOpacity>
        </View>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
};

export default Chats;

const styles = StyleSheet.create({
  chatContainer: {
    padding: 16,
    paddingBottom: 80,
  },
  messageWrapper: {
    marginVertical: 6,
    maxWidth: "80%",
  },
  left: {
    alignSelf: "flex-start",
  },
  right: {
    alignSelf: "flex-end",
  },
  card: {
    padding: 10,
    borderRadius: 10,
  },

  userCard: {
    backgroundColor: "#d2c7ff",
  },

  botCard: {
    backgroundColor: "#E5E5EA",
  },
  messageText: {
    fontSize: 16,
  },
  inputBar: {
    position: "absolute",
    bottom: 0,
    left: 0,
    right: 0,
    flexDirection: "row",
    padding: 10,
    backgroundColor: "#fff",
    borderTopWidth: 1,
    borderColor: "#ddd",
  },
  input: {
    flex: 1,
    minHeight: 40,
    maxHeight: 100,
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderWidth: 1,
    borderColor: "#ccc",
    borderRadius: 20,
    backgroundColor: "#fafafa",
  },
  sendButton: {
    marginLeft: 8,
    backgroundColor: "#7A4DFF",
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 14,
    justifyContent: "center",
    alignItems: "center",
  },
})
