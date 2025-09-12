import { Provider, defaultTheme, View, Heading, Flex, Well } from '@adobe/react-spectrum';
import TaskManager from './components/TaskManager';

function App() {
  return (
    <Provider theme={defaultTheme}>
      <View 
        padding="size-400" 
        minHeight="100vh"
        backgroundColor="gray-50"
      >
        <Flex direction="column" gap="size-300" maxWidth="800px" margin="0 auto">
          <Well>
            <Flex direction="column" gap="size-200">
              <Heading level={1} textAlign="center">
                📋 Task Manager
              </Heading>
            </Flex>
          </Well>
          <TaskManager />
        </Flex>
      </View>
    </Provider>
  );
}

export default App;
