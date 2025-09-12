import React from 'react';
import { View, Text, Flex, ProgressCircle, Heading, Well, Badge, Button } from '@adobe/react-spectrum';
import TaskItem from './TaskItem';

export default function TaskList({ tasks, onUpdateTaskStatus }) {
  const pendingTasks = tasks.filter(task => task.status === 'PENDING');
  const completedTasks = tasks.filter(task => task.status === 'COMPLETED');

  return (
    <View>
      <Flex direction="column" gap="size-200">
        <Well>
          <Flex direction="row" justifyContent="space-between" alignItems="center">
            <Heading level={2}>📝 Tasks ({tasks.length})</Heading>
            <Flex gap="size-150">
              <Badge variant="warning">
                ⏳ Pending: {pendingTasks.length}
              </Badge>
              <Badge variant="success">
                ✅ Completed: {completedTasks.length}
              </Badge>
            </Flex>
          </Flex>
        </Well>

        {tasks.length === 0 ? (
          <Well>
            <View
              padding="size-400"
              textAlign="center"
              backgroundColor="gray-75"
              borderRadius="medium"
            >
              <Text fontSize="size-150" color="gray-600">
                🎯 No tasks found. Create your first task above!
              </Text>
            </View>
          </Well>
        ) : (
          <Flex direction="column" gap="size-100">
            {pendingTasks.length > 0 && (
              <View>
                <Text fontSize="size-100" color="gray-700" fontWeight="semibold" marginBottom="size-100">
                  🔥 Active Tasks ({pendingTasks.length})
                </Text>
                {pendingTasks.map((task) => (
                  <TaskItem key={task.id} task={task} onUpdateTaskStatus={onUpdateTaskStatus} />
                ))}
              </View>
            )}
            
            {completedTasks.length > 0 && (
              <View>
                <Text fontSize="size-100" color="gray-700" fontWeight="semibold" marginBottom="size-100">
                  🏆 Completed Tasks ({completedTasks.length})
                </Text>
                {completedTasks.map((task) => (
                  <TaskItem key={task.id} task={task} onUpdateTaskStatus={onUpdateTaskStatus} />
                ))}
              </View>
            )}
          </Flex>
        )}
      </Flex>
    </View>
  );
}

