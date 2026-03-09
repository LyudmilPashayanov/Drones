using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class JobsListView : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private JobsListItemView itemPrefab;

    private readonly Dictionary<Job, JobsListItemView> _jobItems = new();
    private JobsViewModel _vm;

    [Inject]
    public void Construct(JobsViewModel vm)
    {
        _vm = vm;
    }

    void Start()
    {
        _vm.JobAdded += AddItem;
        _vm.JobUpdated += UpdateJobState;

        foreach (var job in _vm.Jobs)
        {
            AddItem(job);
        }
    }

    private void UpdateJobState(Job job)
    {
        if (_jobItems.TryGetValue(job, out var item))
        {
            item.UpdateState(job.Status.ToString());
        }
    }

    private void AddItem(Job job)
    {
        JobsListItemView item = Instantiate(itemPrefab, content);

        // convert Job → JobData for UI
        item.Initialize(job, _vm);

        _jobItems.Add(job, item);

        ResizeScrollView(item.GetComponent<RectTransform>());
    }

    private void ResizeScrollView(RectTransform itemRect)
    {
        Vector2 currentSize = content.sizeDelta;
        currentSize.y += itemRect.rect.height + 20;
        content.sizeDelta = currentSize;

        itemRect.anchoredPosition = new Vector2(
            itemRect.anchoredPosition.x,
            -(content.sizeDelta.y - itemRect.rect.height - 20)
        );
    }
}