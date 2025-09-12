import React from 'react';
import { View, Flex, ProgressCircle, Text, Well } from '@adobe/react-spectrum';
import { useTasks } from '../hooks/useTasks';
import TaskForm from './TaskForm';
import TaskList from './TaskList';

export default function TaskManager() {
  const { 
    tasks, 
    loading, 
    error, 
    createTask, 
    updateTaskStatus 
  } = useTasks();

  if (loading) {
    return (
      <View padding="size-400" textAlign="center">
        <ProgressCircle aria-label="Loading..." isIndeterminate />
        <Text>Loading task manager...</Text>
      </View>
    );
  }

  if (error) {
    return (
      <Well>
        <View padding="size-400" textAlign="center">
          <Text color="negative" fontSize="size-150">
            ❌ Error: {error}
          </Text>
          <Text fontSize="size-100" marginTop="size-100">
            Please check your API connection and try again.
          </Text>
        </View>
      </Well>
    );
  }

  return (
    <View>
      <Flex direction="column" gap="size-300">
        <TaskForm onAddTask={createTask} />
        <TaskList tasks={tasks} onUpdateTaskStatus={updateTaskStatus} />
      </Flex>
    </View>
  );
}

