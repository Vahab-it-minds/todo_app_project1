import type { Todo, TodoStatus } from '../services/todos'
import { useDroppable } from '@dnd-kit/core'
import TodoCard from './TodoCard'

type KanbanColumnProps = {
  title: string
  status: TodoStatus
  todos: Todo[]
  onTodoClick: (todo: Todo) => void
}

function KanbanColumn({ title, status, todos, onTodoClick, }: KanbanColumnProps) {
  const {
    setNodeRef,
    isOver,
  } = useDroppable({
      id: status,
  })
  return (
    <section
      ref={setNodeRef}
      className={`kanban-column ${isOver ? 'kanban-column-over' : ''}`}
    >
      <div className="kanban-column-header">
        <h2>{title}</h2>
        <span className="task-count">{todos.length}</span>
      </div>

      <div className="kanban-column-content">
        {todos.map((todo) => (
          <TodoCard
            key={todo.id}
            todo={todo}
            onClick={() => onTodoClick(todo)}
          />
        ))}
      </div>
    </section>
  )
}

export default KanbanColumn